﻿using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string type)
        {
            return claimsPrincipal?.FindAll(type)?.Select(x => x.Value).ToList();
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role).ToList();
        }

        public static string ClaimEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value ?? "<Anonymous>";
        }

        public static int ClaimNameIdentifier(this ClaimsPrincipal claimsPrincipal)
        {
            return Convert.ToInt32(claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
