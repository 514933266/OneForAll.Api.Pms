using Pms.HttpService.Interfaces;
using Microsoft.AspNetCore.Http;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Pms.HttpService.Models;

namespace Pms.HttpService.Models
{
    /// <summary>
    /// Http服务：团队
    /// </summary>
    public class OATeamHttpService : IOATeamHttpService
    {
        private readonly HttpServiceConfig _config;
        private readonly string AUTH_KEY = "Authorization";
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHttpClientFactory _httpClientFactory;
        public OATeamHttpService(
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
        /// 获取团队列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="justValid">是否有效</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OATeam>> GetListAsync(int type = -1, bool justValid = true)
        {
            if (!Token.IsNullOrEmpty())
            {
                var client = _httpClientFactory.CreateClient(_config.OATeam);
                client.DefaultRequestHeaders.Add(AUTH_KEY, Token);
                var result = await client.GetStringAsync(client.BaseAddress + "?type={1}&justValid={2}".Fmt(Guid.Empty, type, justValid));
                return result.FromJson<IEnumerable<OATeam>>();
            }
            return new List<OATeam>();
        }

        /// <summary>
        /// 获取团队树列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="justValid">是否有效</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OATeamTree>> GetListTreeAsync(int type = -1, bool justValid = true)
        {
            if (!Token.IsNullOrEmpty())
            {
                var client = _httpClientFactory.CreateClient(_config.OATeam);
                client.DefaultRequestHeaders.Add(AUTH_KEY, Token);
                var result = await client.GetStringAsync(client.BaseAddress + "/{0}/TreeNodes?type={1}&justValid={2}".Fmt(Guid.Empty, type, justValid));
                return result.FromJson<IEnumerable<OATeamTree>>();
            }
            return new List<OATeamTree>();
        }

        /// <summary>
        /// 获取团队实体
        /// </summary>
        /// <returns>人员列表</returns>
        public async Task<OATeam> GetAsync(Guid id)
        {
            if (!Token.IsNullOrEmpty())
            {
                var client = _httpClientFactory.CreateClient(_config.OATeam);
                client.DefaultRequestHeaders.Add(AUTH_KEY, Token);
                var result = await client.GetStringAsync(client.BaseAddress + "/{0}".Fmt(id));
                return result.FromJson<OATeam>();
            }
            return null;
        }
    }
}
