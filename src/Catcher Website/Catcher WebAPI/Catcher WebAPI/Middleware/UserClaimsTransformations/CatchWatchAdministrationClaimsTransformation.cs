using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Catcher_WebAPI.Middleware.UserClaimsTransformations
{
    public class CatchWatchAdministrationClaimsTransformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.IsInRole("Administrators"))
            {
                ((List<Claim>) principal.Claims).Add(new Claim("IsAdmin", "true"));
            }

            return Task.FromResult(principal);
        }
    }
}
