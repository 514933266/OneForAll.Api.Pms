using AutoMapper;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using OneForAll.Core;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Aggregates;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.HPSF;
using OneForAll.Core.Upload;
using OneForAll.File;
using System.Drawing;
using NPOI.SS.Formula.UDF;
using Pms.Domain.Enums;
using System.IO.Compression;
using OneForAll.Core.OAuth;
using Microsoft.AspNetCore.Http;
using OneForAll.Core.Utility;
using Pms.Domain;
using Pms.Domain.ValueObjects;
using NPOI.SS.Formula.PTG;

namespace Pms.Domain
{
    /// <summary>
    /// 代码结构
    /// </summary>
    public class PmsCodeStructureManager : PmsBaseManager, ISysCodeStructureManager
    {
        private readonly IPmsCodeStructureRepository _repository;
        private readonly IPmsCodeDenerationRecordRepository _recordRepository;
        private readonly IPmsEntityTableRepository _tableRepository;

        // 文件存储路径
        private readonly string UPLOAD_PATH = "/upload/projects/{0}/codes/";
        // 虚拟路径：根据Startup配置设置,返回给前端访问资源
        private readonly string VIRTUAL_PATH = "/resources/projects/{0}/codes/";
        public PmsCodeStructureManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IPmsCodeStructureRepository repository,
            IPmsCodeDenerationRecordRepository recordRepository,
            IPmsEntityTableRepository tableRepository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _recordRepository = recordRepository;
            _tableRepository = tableRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<PmsCodeStructure>> GetListAsync(Guid projectId, string key)
        {
            return await _repository.GetListAsync(projectId, key);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">菜单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsCodeStructureForm form)
        {
            var exists = await CheckParentExists(form);
            if (!exists) return BaseErrType.DataNotFound;

            var data = _mapper.Map<PmsCodeStructureForm, PmsCodeStructure>(form);
            data.PmsProjectId = projectId;
            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;
            if (data.Type == PmsCodeStructureTypeEnum.Folder && !data.TemplateJson.IsNullOrWhiteSpace())
            {
                data.TemplateJson = string.Empty;
            }
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">菜单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsCodeStructureForm form)
        {
            var exists = await CheckParentExists(form);
            if (!exists)
                return BaseErrType.DataNotFound;
            var data = await _repository.FindAsync(form.Id);
            if (data == null)
                return BaseErrType.DataNotFound;
            if (data.Type == PmsCodeStructureTypeEnum.Folder && !data.TemplateJson.IsNullOrWhiteSpace())
            {
                data.TemplateJson = string.Empty;
            }

            _mapper.Map(form, data);
            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;
            data.UpdateTime = DateTime.Now;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        private async Task<bool> CheckParentExists(PmsCodeStructureForm form)
        {
            if (form.ParentId != Guid.Empty)
            {
                var data = await _repository.GetListAsync();
                var parent = data.FirstOrDefault(w => w.Id == form.ParentId);
                var children = data.FindChildren(form.Id);

                // 1. 禁止选择不存在的上级
                // 2. 禁止选择下级作为自己的上级
                if (parent == null) return false;
                if (form.Id != Guid.Empty &&
                    children.Any(w => w.Id.Equals(form.ParentId))) return false;
            }
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">菜单id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids)
        {
            var canDel = true;
            var errType = BaseErrType.Success;
            var data = await _repository.GetListAsync();
            var delData = data.Where(w => ids.Contains(w.Id)).ToList();
            if (data.Count() < 1 || delData.Count < 1)
                return BaseErrType.DataEmpty;

            for (var i = 0; i < delData.Count; i++)
            {
                var element = delData[i];
                var children = data.FindChildren(element.Id);
                if (children.Count() > 0)
                {
                    canDel = false;
                    errType = BaseErrType.DataExist;
                    break;
                }
            }
            if (canDel)
            {
                errType = await ResultAsync(() => _repository.DeleteRangeAsync(delData));
            }
            return errType;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> CreateAsync(Guid projectId, PmsCodeStructureCreateForm form)
        {
            var data = await _repository.GetListAsync();
            if (!data.Any())
                return BaseErrType.DataEmpty;

            var name = DateTime.Now.ToString("yyyyMMddHHmm");
            var path = AppDomain.CurrentDomain.BaseDirectory + UPLOAD_PATH.Fmt(projectId) + name;
            DirectoryHelper.Create(path);

            var treeData = _mapper.Map<IEnumerable<PmsCodeStructure>, IEnumerable<PmsCodeStructureTreeAggr>>(data);
            var tree = treeData.ToTree<PmsCodeStructureTreeAggr, Guid>();

            var success = false;
            var tables = treeData.ToList();
            if (form.Ids.Any())
            {
                tables = treeData.Where(w => form.Ids.Contains(w.Id)).ToList();
            }

            tables.ForEach(e =>
            {
                var fieldBody = StringHelper.MatchMiddleValue(e.TemplateJson, "\\[FieldBody\\]", "\\[FieldBody\\]");
                if (!fieldBody.IsNullOrEmpty())
                {
                    success = CreateFile(e.Name, e.Remark, fieldBody, path, tree);
                }
            });

            if (success)
            {
                var filename = name + ".zip";
                var zipName = path + ".zip";
                if (!FileHelper.CheckIsExists(zipName))
                {
                    ZipFile.CreateFromDirectory(path, zipName);
                    return await ResultAsync(() => _recordRepository.AddAsync(new PmsCodeDenerationRecord()
                    {
                        Title = filename,
                        PmsProjectId = projectId,
                        CreatorId = LoginUser.Id,
                        CreatorName = LoginUser.Name,
                        Url = VIRTUAL_PATH.Fmt(projectId) + filename
                    }));
                }
                else
                {
                    return BaseErrType.DataExist;
                }
            }
            return BaseErrType.Fail;
        }

        // 创建目录和代码文件
        private bool CreateFile(string formName, string comment, string fieldBody, string parentPath, IEnumerable<PmsCodeStructureTreeAggr> data)
        {
            var success = true;
            try
            {
                data.ForEach(e =>
                {
                    var path = Path.Combine(parentPath, e.Name.Replace($"[EntityName]", formName));
                    if (e.Type == PmsCodeStructureTypeEnum.Folder)
                    {
                        DirectoryHelper.Create(path);
                        if (e.Children.Any())
                        {
                            success = CreateFile(formName, comment, fieldBody, path, e.Children);
                        }
                    }
                    else if (e.Type == PmsCodeStructureTypeEnum.File)
                    {
                        var extension = Path.GetExtension(e.Name);
                        if (!extension.IsNullOrEmpty())
                        {
                            var content = e.TemplateJson
                                .Replace($"[EntityName]", formName)
                                .Replace($"[Comment]", comment)
                                .Replace($"[FieldBody]", fieldBody);
                            FileHelper.Write(path, Encoding.UTF8.GetBytes(content));
                        }
                    }
                });
            }
            catch
            {
                success = false;
            }
            return success;
        }
    }
}
