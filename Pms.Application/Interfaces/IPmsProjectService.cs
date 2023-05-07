using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Aggregates;
using Pms.Domain.Models;
using OneForAll.Core;
using OneForAll.Core.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Pms.Public.Models;
using Pms.Domain.Enums;

namespace Pms.Application.Interfaces
{
    /// <summary>
    /// 应用服务：实体
    /// </summary>
    public interface IPmsProjectService
    {
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="scope">项目范围</param>
        /// <param name="name">项目名称</param>
        /// <returns>项目列表</returns>
        Task<IEnumerable<PmsProjectDto>> GetListAsync(PmsProjectVisitEnum scope, string name);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(PmsProjectForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(PmsProjectForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid id);

        /// <summary>
        /// 设为星标实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> SetToStarAsync(Guid id);

        /// <summary>
        /// 获取实体成员任务统计
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns></returns>
        Task<IEnumerable<PmsMemberTaskStatisticsDto>> GetMemberStatisticsAsync(Guid id);

    }
}
