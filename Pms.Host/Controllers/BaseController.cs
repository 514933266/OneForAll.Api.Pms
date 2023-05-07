using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pms.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Pms.HttpService.Models;
using Pms.Public.Models;

namespace Pms.Host.Controllers
{
    public class BaseController : Controller
    {
        protected Guid SysUserId
        {
            get
            {
                var userId = HttpContext
                .User
                .Claims
                .FirstOrDefault(e => e.Type == UserClaimType.USER_ID);

                if (userId != null)
                {
                    return new Guid(userId.Value);
                }
                return Guid.Empty;
            }
        }

        protected string UserName
        {
            get
            {
                var username = HttpContext
                .User
                .Claims
                .FirstOrDefault(e => e.Type == UserClaimType.USERNAME);

                if (username != null)
                {
                    return username.Value;
                }
                return null;
            }
        }

        protected Guid SysTenantId
        {
            get
            {
                var tenantId = HttpContext
                .User
                .Claims
                .FirstOrDefault(e => e.Type == UserClaimType.TENANT_ID);

                if (tenantId != null)
                {
                    return new Guid(tenantId.Value);
                }
                return Guid.Empty;
            }
        }


        protected LoginUser LoginUser
        {
            get
            {
                var name = HttpContext
                .User
                .Claims
                .FirstOrDefault(e => e.Type == UserClaimType.USER_NICKNAME);

                var role = HttpContext
                .User
                .Claims
                .FirstOrDefault(e => e.Type == UserClaimType.ROLE);

                return new LoginUser()
                {
                    Id = SysUserId,
                    Name = name.Value,
                    TenantId = SysTenantId
                };
            }
        }
        public static string GetModelStateFirstError(ModelStateDictionary modelState)
        {
            var error = modelState.Where(m => m.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors }).FirstOrDefault().Errors.First();
            return error.ErrorMessage.IsNullOrEmpty() ? error.Exception.Message : error.ErrorMessage;
        }
    }
}