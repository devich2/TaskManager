using System;
using System.Security.Claims;

namespace TaskManager.Common.Utils
{
    public static class ClaimsExtensions
    {
        public static Tuple<int, string> UnpackRole(this Claim claim)
        {
            string[] projectRole = claim.Value.Split("_", StringSplitOptions.RemoveEmptyEntries);
            if(projectRole.Length != 2 || !int.TryParse(projectRole[1], out int projectId))
                return  null;
            return new Tuple<int, string>(projectId, projectRole[0]);
        }
    }
}