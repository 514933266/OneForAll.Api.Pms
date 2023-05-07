using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.Host
{
    /// <summary>
    /// 供应器：租户
    /// </summary>
    public interface ITenantProvider
    {
        Guid GetTenantId();
    }
}
