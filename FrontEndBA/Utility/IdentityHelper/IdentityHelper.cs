using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Utility
{
    public static class IdentityHelper
    {
        public static int getUserID(ClaimsPrincipal user)
        {
            var identity = (ClaimsIdentity)user.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            return Convert.ToInt32(claims.ElementAt(3).Value);
        }
    }
}
