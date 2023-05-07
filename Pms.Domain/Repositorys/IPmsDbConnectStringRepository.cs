using OneForAll.EFCore;
using Pms.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Repositorys
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public interface IPmsDbConnectStringRepository : IEFCoreRepository<PmsDbConnectString>
    {
    }
}
