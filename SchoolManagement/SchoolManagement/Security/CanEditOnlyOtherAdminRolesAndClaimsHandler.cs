using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Security
{
    public class CanEditOnlyOtherAdminRolesAndClaimsHandler : AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CanEditOnlyOtherAdminRolesAndClaimsHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageAdminRolesAndClaimsRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var loggedAdminId = context.User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.NameIdentifier)?.Value;

            var adminIdBeingEdited = httpContext.Request.Query["userId"].ToString();

            if (context.User.IsInRole("Admin") &&
                context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true"))
            {
                if (string.IsNullOrEmpty(adminIdBeingEdited))
                {
                    context.Succeed(requirement);
                }
                else if(adminIdBeingEdited.ToLower() != loggedAdminId?.ToLower())
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
