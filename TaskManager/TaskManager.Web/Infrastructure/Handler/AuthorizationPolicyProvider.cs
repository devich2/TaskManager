using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace TaskManager.Web.Infrastructure.Handler
{
    public class AuthorizationPolicyProvider: DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;

        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            _options = options.Value;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            //Unit tested shows this is quicker (and safer - see link to issue above) than the original version
            return await base.GetPolicyAsync(policyName) 
                   ?? new AuthorizationPolicyBuilder()
                       .AddRequirements(new PermissionRequirement(policyName))
                       .Build();
        }
    }
}