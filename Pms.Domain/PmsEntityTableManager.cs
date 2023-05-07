using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.Upload;
using OneForAll.EFCore;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Aggregates;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using Pms.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Geom;

namespace Pms.Domain
{
    /// <summary>
    /// 表实体
    /// </summary>
    public class PmsEntityTableManager : PmsBaseManager, IPmsEntityTableManager
    {
        private readonly IPmsEntityTableRepository _repository;
        private readonly IPmsEntityTableContactRepository _contactRepository;
        public PmsEntityTableManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IPmsEntityTableRepository repository,
            IPmsEntityTableContactRepository contactRepository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns>Bug分页</returns>
        public async Task<IEnumerable<PmsEntityTable>> GetListAsync(Guid projectId, string key)
        {
            return await _repository.GetListAsync(projectId, key);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsEntityTableForm form)
        {
            var data = _mapper.Map<PmsEntityTableForm, PmsEntityTable>(form);

            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;
            data.PmsProjectId = projectId;
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsEntityTableForm form)
        {
            var data = await _repository.FindAsync(form.Id);
            if (data == null)
                return BaseErrType.DataNotFound;

            _mapper.Map(form, data);
            data.UpdateTime = DateTime.Now;
            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListAsync(w => w.PmsProjectId == projectId && ids.Contains(w.Id));
            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 修改表字段
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateFieldAsync(Guid projectId, Guid id, PmsEntityTableUpdateFieldForm form)
        {
            var data = await _repository.FindAsync(id);
            if (data == null)
                return BaseErrType.DataNotFound;

            _mapper.Map(form, data);
            data.UpdateTime = DateTime.Now;
            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 修改关联
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <param name="targetIds">关联表id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateContactAsync(Guid projectId, Guid id, IEnumerable<Guid> targetIds)
        {
            var data = await _repository.FindAsync(id);
            if (data == null)
                return BaseErrType.DataNotFound;

            var exists = await _contactRepository.GetListAsync(projectId, id);
            var addData = new List<PmsEntityTableContact>();
            targetIds.ForEach(e =>
            {
                if (!exists.Any(w => w.SourceTableId == e || w.TargetTableId == e))
                {
                    addData.Add(new PmsEntityTableContact()
                    {
                        PmsProjectId = projectId,
                        SourceTableId = id,
                        TargetTableId = e
                    });
                }
            });

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                if (addData.Any())
                    await _contactRepository.AddRangeAsync(addData, tran);
                return await ResultAsync(() => tran.CommitAsync());
            }
        }

        /// <summary>
        /// 删除关联
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <param name="contactIds">关联表id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteContactAsync(Guid projectId, Guid id, IEnumerable<Guid> contactIds)
        {
            var data = await _repository.FindAsync(id);
            if (data == null)
                return BaseErrType.DataNotFound;

            var exists = await _contactRepository.GetListAsync(projectId, id);
            var delData = exists.Where(w => contactIds.Contains(w.SourceTableId) || contactIds.Contains(w.TargetTableId)).ToList();
            if (!delData.Any())
                return BaseErrType.DataEmpty;

            return await ResultAsync(() => _contactRepository.DeleteRangeAsync(delData));
        }

        /// <summary>
        /// 自动根据连接字符串生成
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="connectString">连接字符串</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> CreateAsync(Guid projectId, string connectString)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT\r\n");
            sql.Append("TableName =d.name,\r\n");
            sql.Append("TableComment = CASE WHEN a.colorder = 1 then ISNULL(f.value, '') ELSE '' END,\r\n");
            sql.Append("FieldSortNumber = a.colorder,\r\n");
            sql.Append("[Name] = a.name,\r\n");
            sql.Append("IsIdentity = COLUMNPROPERTY(a.id, a.name, 'IsIdentity'),\r\n");
            sql.Append("IsPrimaryKey = CASE WHEN EXISTS(SELECT 1 FROM sysobjects WHERE xtype = 'PK' AND parent_obj = a.id AND name in (SELECT name FROM sysindexes WHERE indid IN(SELECT indid FROM sysindexkeys WHERE id = a.id AND colid = a.colid))) THEN 1 ELSE 0 END,\r\n");
            sql.Append("[Type] = b.name,\r\n");
            sql.Append("[Length] = COLUMNPROPERTY(a.id, a.name, 'PRECISION'),\r\n");
            sql.Append("[Precision] = ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0),\r\n");
            sql.Append("IsNullable = a.isnullable,\r\n");
            sql.Append("DefaultValue = ISNULL(e.text, ''),\r\n");
            sql.Append("Comment = ISNULL(g.[value], '')\r\n");
            sql.Append("FROM syscolumns a\r\n");
            sql.Append("LEFT JOIN systypes b ON a.xusertype = b.xusertype\r\n");
            sql.Append("INNER JOIN sysobjects d ON a.id = d.id  and d.xtype = 'U' and d.name <> 'dtproperties'\r\n");
            sql.Append("LEFT JOIN syscomments e ON a.cdefault = e.id\r\n");
            sql.Append("LEFT JOIN sys.extended_properties g ON a.id = g.major_id and a.colid = g.minor_id\r\n");
            sql.Append("LEFT JOIN sys.extended_properties f ON d.id = f.major_id and f.minor_id = 0\r\n");
            sql.Append("ORDER BY a.id,a.colorder;");

            var data = await _repository.GetListAsync(projectId, string.Empty);

            var result = new List<PmsTableFieldInfoVo>();
            using (var conn = new SqlConnection(connectString))
            {
                conn.Open();
                result = conn.Query<PmsTableFieldInfoVo>(sql.ToString()).ToList();
            }

            var addList = new List<PmsEntityTable>();
            var updList = new List<PmsEntityTable>();
            var groups = result.GroupBy(g => g.TableName).Select(s => new
            {
                s.Key,
                s.FirstOrDefault().TableComment,
                Fields = s.Select(x => new PmsEntityFieldVo()
                {
                    Name = x.Name,
                    DbType = ConvertDbTypeString(x.Type, x.Length, x.Precision),
                    Alias = x.Comment,
                    Remark = x.Comment,
                    IsPrimaryKey = x.IsPrimaryKey,
                    IsIdentity = x.IsIdentity,
                    IsNullable = x.IsNullable,
                    DefaultValue = x.DefaultValue,
                    SortNumber = x.SortNumber
                }).ToList()
            }).ToList();

            groups.ForEach(e =>
            {
                var item = data.FirstOrDefault(w => w.Name == e.Key);
                if (item != null)
                {
                    if (!e.TableComment.IsNullOrEmpty())
                    {
                        item.Remark = e.TableComment;
                    }
                    item.CreatorId = LoginUser.Id;
                    item.CreatorName = LoginUser.Name;
                    item.UpdateTime = DateTime.Now;
                    item.FiledJson = e.Fields.ToJson();
                    updList.Add(item);
                }
                else
                {
                    addList.Add(new PmsEntityTable()
                    {
                        Name = e.Key,
                        PmsProjectId = projectId,
                        Alias = e.TableComment,
                        Remark= e.TableComment,
                        CreatorId = LoginUser.Id,
                        CreatorName = LoginUser.Name,
                        UpdateTime = DateTime.Now,
                        FiledJson = e.Fields.ToJson()
                    });
                }
            });

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                if (addList.Any())
                    await _repository.AddRangeAsync(addList, tran);

                if (updList.Any())
                    await _repository.UpdateRangeAsync(updList, tran);
                return await ResultAsync(tran.CommitAsync);
            };
        }

        private string ConvertDbTypeString(string type, int length, int precision)
        {
            if (type == "varchar" || type == "nvarchar")
            {
                return $"{type}({(length == -1 ? "max" : length)})";
            }
            else if (type == "decimal")
            {
                return $"{type}({length},{precision})";
            }
            return type;
        }
    }
}
