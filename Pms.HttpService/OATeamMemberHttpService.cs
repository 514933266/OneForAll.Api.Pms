using Pms.HttpService.Interfaces;
using Pms.HttpService.Models;
using Microsoft.AspNetCore.Http;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pms.HttpService
{
    /// <summary>
    /// Http服务：团队成员
    /// </summary>
    public class OATeamMemberHttpService : IOATeamMemberHttpService
    {
        private readonly HttpServiceConfig _config;
        private readonly string AUTH_KEY = "Authorization";
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHttpClientFactory _httpClientFactory;
        public OATeamMemberHttpService(
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
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OATeamMember>> GetListAsync()
        {
            if (!Token.IsNullOrEmpty())
            {
                var client = _httpClientFactory.CreateClient("OATeamMember");
                client.DefaultRequestHeaders.Add(AUTH_KEY, Token);
                var result = await client.GetStringAsync(client.BaseAddress);
                return result.FromJson<IEnumerable<OATeamMember>>();
            }
            return new List<OATeamMember>();
        }

        /// <summary>
        /// 获取列表（基础信息）
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OATeamMember>> GetListAsync(string key)
        {
            if (!Token.IsNullOrEmpty())
            {
                var client = _httpClientFactory.CreateClient(_config.OATeamMember);
                client.DefaultRequestHeaders.Add(AUTH_KEY, Token);
                var result = await client.GetStringAsync(client.BaseAddress + "?justValid={0}&key={1}".Fmt(false, key));
                return result.FromJson<IEnumerable<OATeamMember>>();
            }
            return new List<OATeamMember>();
        }

        /// <summary>
        /// 获取列表（基础信息）
        /// </summary>
        /// <param name="endDate">结束时间</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OATeamMember>> GetListAsync(DateTime endDate)
        {
            if (!Token.IsNullOrEmpty())
            {
                var client = _httpClientFactory.CreateClient(_config.OATeamMember);
                client.DefaultRequestHeaders.Add(AUTH_KEY, Token);
                var result = await client.GetStringAsync(client.BaseAddress + "?justValid={0}&endDate={1}".Fmt(false, endDate));
                return result.FromJson<IEnumerable<OATeamMember>>();
            }
            return new List<OATeamMember>();
        }
    }
}
