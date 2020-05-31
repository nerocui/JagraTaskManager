using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Helpers
{
    static public class Extensions
    {
        static public ClaimsPrincipal GetPrincipal(this string userId)
        {
            var claimPrincipal = new ClaimsPrincipal();
            var claimIdentity = new ClaimsIdentity();
            var claim = new Claim(ClaimTypes.NameIdentifier, userId);
            claimIdentity.AddClaim(claim);
            claimPrincipal.AddIdentity(claimIdentity);
            return claimPrincipal;
        }

        static public string GetUserId(this HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var identityClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return identityClaim.Value;
        }
    }
}
