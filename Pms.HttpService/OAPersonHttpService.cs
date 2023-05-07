using Pms.HttpService.Interfaces;
using Microsoft.AspNetCore.Http;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core;
using System.Net.Http.Formatting;
using Pms.HttpService.Models;

namespace Pms.HttpService
{
    /// <summary>
    /// OA人员资料服务
    /// </summary>
    public class OAPersonHttpService: IOAPersonHttpService
    {
        private readonly HttpServiceConfig _config;
        private readonly string AUTH_KEY = "Authorization";
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHttpClientFactory _httpClientFactory;
        public OAPersonHttpService(
            HttpServiceConfig config,
            IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpContext = httpContext;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 登录token
        /// </summary>
        private string Token
        {
            get
            {
                var context = _httpContext.HttpContext;
                if (context != null)
                {
                    return context.Request.Headers
                      .FirstOrDefault(w => w.Key.Equals(AUTH_KEY))
                      .Value.TryString();
                }
                return "";
            }
        }

        /// <summary>
        /// 获取列表（基础信息）
        /// </summary>
        /// <param name="id">人员id</param>
        /// <returns>人员</returns>
        public async Task<OAPersonBasicInfo> GetAsync(Guid id)
        {
            if (!Token.IsNullOrEmpty())
            {
                var client = _httpClientFactory.CreateClient("OAPerson");
                client.DefaultRequestHeaders.Add(AUTH_KEY, Token);
                var result = await client.GetStringAsync(client.BaseAddress + "/{0}".Fmt(id));
                return result.FromJson<OAPersonBasicInfo>();
            }
            return null;
        }


        /// <summary>
        /// 获取列表（基础信息）
        /// </summary>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OAPersonBasicInfo>> GetListAsync()
        {
            if (!Token.IsNullOrEmpty())
            {
                var client = _httpClientFactory.CreateClient("OAPerson");
                client.DefaultRequestHeaders.Add(AUTH_KEY, Token);
                var result = await client.GetStringAsync(client.BaseAddress);
                return result.FromJson<IEnumerable<OAPersonBasicInfo>>();
            }
            return new List<OAPersonBasicInfo>();
        }

        /// <summary>
        /// 获取列表（基础信息）
        /// </summary>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OAPersonBasicInfo>> GetInserviceListAsync(IEnumerable<Guid> ids)
        {
            if (!Token.IsNullOrEmpty())
            {
                var data = await GetInserviceListAsync();
                return data.Where(w => w.Id.In(ids));
            }
            return new List<OAPersonBasicInfo>();
        }

        /// <summary>
        /// 获取列表（基础信息）
        /// </summary>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OAPersonBasicInfo>> GetInserviceListAsync()
        {
            if (!Token.IsNullOrEmpty())
            {
                var client = _httpClientFactory.CreateClient("OAPerson");
                client.DefaultRequestHeaders.Add(AUTH_KEY, Token);

                var url = new Uri("{0}/Inservice".Fmt(client.BaseAddress.ToString()));
                var result = await client.GetStringAsync(url);
                return result.FromJson<IEnumerable<OAPersonBasicInfo>>();
            }
            return new List<OAPersonBasicInfo>();
        }

        /// <summary>
        /// 设置离职
        /// </summary>
        /// <param name="id">人员id</param>
        /// <returns>结果</returns>
        public async Task<BaseMessage> SetLeaveAsync(Guid id)
        {
            if (!Token.IsNullOrEmpty())
            {
                var client = _httpClientFactory.CreateClient(_config.OAPerson);
                client.DefaultRequestHeaders.Add(AUTH_KEY, Token);
                var url = new Uri("{0}/{1}/LeaveNow".Fmt(client.BaseAddress.ToString(), id));
                var response = await client.PatchAsync(url, null);
                string result = await response.Content.ReadAsStringAsync();
                return result.FromJson<BaseMessage>();
            }
            return new BaseMessage();
        }
    }
}
