using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Repository
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public class PmsDbConnectStringRepository : Repository<PmsDbConnectString>, IPmsDbConnectStringRepository
    {
        public PmsDbConnectStringRepository(DbContext context)
            : base(context)
        {

        }
    }
}
