using System;
using System.Security.Claims;
using System.Security.Principal;

namespace TaskManager.Common.Utils
{
    public static class UserHelper
    {
        public static int GetUserId(this IPrincipal principal)
        {
            ClaimsIdentity claimsIdentity =
                (ClaimsIdentity)principal.Identity;
            Claim claim =
                claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return Int32.Parse(claim.Value);
        }
    }
}