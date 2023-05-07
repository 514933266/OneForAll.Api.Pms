using Pms.Domain.AggregateRoots;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Repositorys
{
    /// <summary>
    /// 仓储：任务附件
    /// </summary>
    public interface IPmsTaskFileRepository : IEFCoreRepository<PmsTaskFile>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <returns>结果</returns>
        Task<IEnumerable<PmsTaskFile>> GetListAsync(Guid taskId);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="taskIds">任务id</param>
        /// <returns>结果</returns>
        Task<IEnumerable<PmsTaskFile>> GetListByTaskAsync(IEnumerable<Guid> taskIds);
    }
}
