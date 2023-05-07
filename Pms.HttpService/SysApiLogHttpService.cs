using Microsoft.AspNetCore.Http;
using Pms.HttpService.Interfaces;
using Pms.HttpService.Models;
using Pms.Public.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace Pms.HttpService
{
    /// <summary>
    /// Api日志
    /// </summary>
    public class SysApiLogHttpService : BaseHttpService, ISysApiLogHttpService
    {
        private readonly HttpServiceConfig _config;

        public SysApiLogHttpService(
            HttpServiceConfig config,
            IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory) : base(httpContext, httpClientFactory)
        {
            _config = config;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task AddAsync(SysApiLogForm entity)
        {
            try
            {
                entity.CreatorId = LoginUser.Id;
                entity.CreatorName = LoginUser.Name;
                entity.SysTenantId = LoginUser.TenantId;

                var client = GetHttpClient(_config.SysApiLog);
                if (client != null)
                    await client.PostAsync(client.BaseAddress, entity, new JsonMediaTypeFormatter());
            }
            catch
            {

            }
        }
    }
}

