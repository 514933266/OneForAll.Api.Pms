using Microsoft.AspNetCore.Http;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.Security;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Pms.HttpService.Interfaces;
using Pms.HttpService.Models;
using Pms.Public.Models;

namespace Pms.HttpService
{
    /// <summary>
    /// Http服务：租户
    /// </summary>
    public class SysTenantHttpService : ISysTenantHttpService
    {
        private readonly string AUTH_KEY = "Authorization";
        private readonly AuthConfig _authConfig;
        private readonly HttpServiceConfig _config;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHttpClientFactory _httpClientFactory;

        public SysTenantHttpService(
            AuthConfig authConfig,
            HttpServiceConfig config,
            IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory)
        {
            _authConfig = authConfig;
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
        /// 添加
        /// </summary>
        /// <returns>返回消息</returns>
        public async Task<BaseMessage> AddAsync(SysTenantForm postData)
        {
            if (!Token.IsNullOrEmpty())
            {
                var claims = _httpContext.HttpContext.User.Claims;
                var uid = claims.FirstOrDefault(e => e.Type == UserClaimType.USER_ID).Value;

                var client = _httpClientFactory.CreateClient(_config.SysTenant);
                var sign = "clientId={0}&clientSecret={1}&apiName={2}&tt={3}".Fmt(_authConfig.ClientId, _authConfig.ClientSecret, _authConfig.ApiName, DateTime.Now.ToString("yyyyMMddhhmm")).ToMd5();
                client.DefaultRequestHeaders.Add(AUTH_KEY, Token);
                client.DefaultRequestHeaders.Add("Unchecked", sign);
                var result = await client.PostAsync(client.BaseAddress, postData, new JsonMediaTypeFormatter());
                return await result.Content.ReadAsAsync<BaseMessage>();
            }
            return new BaseMessage()
            {
                Status = false,
                ErrType = BaseErrType.TokenInvalid,
                Message = "登录已失效，权限验证失败"
            };
        }

        /// <summary>
        /// 更新（存在时会更新）
        /// </summary>
        /// <returns>返回消息</returns>
        public async Task<BaseMessage> UpdateAsync(SysTenantForm postData)
        {
            if (!Token.IsNullOrEmpty())
            {
                var claims = _httpContext.HttpContext.User.Claims;
                var uid = claims.FirstOrDefault(e => e.Type == UserClaimType.USER_ID).Value;

                var client = _httpClientFactory.CreateClient(_config.SysTenant);
                client.DefaultRequestHeaders.Add(AUTH_KEY, Token);
                var result = await client.PutAsync(client.BaseAddress, postData, new JsonMediaTypeFormatter());
                return await result.Content.ReadAsAsync<BaseMessage>();
            }
            return new BaseMessage()
            {
                Status = false,
                ErrType = BaseErrType.TokenInvalid,
                Message = "登录已失效，权限验证失败"
            };
        }
    }
}
