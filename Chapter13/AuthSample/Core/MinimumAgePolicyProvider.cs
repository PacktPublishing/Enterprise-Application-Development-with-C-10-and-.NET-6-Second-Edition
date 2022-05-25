﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AuthSample.Core
{
    public class MinimumAgePolicyProvider : IAuthorizationPolicyProvider
    {
        const string PREFIX = "Over";
        private DefaultAuthorizationPolicyProvider BackupPolicyProvider { get; }

        public MinimumAgePolicyProvider(IOptions<AuthorizationOptions> options)
        {
            this.BackupPolicyProvider =
                new DefaultAuthorizationPolicyProvider(options);
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        => this.BackupPolicyProvider.GetDefaultPolicyAsync();
        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        => this.BackupPolicyProvider.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(PREFIX, StringComparison.OrdinalIgnoreCase) &&
            int.TryParse(policyName.Substring(PREFIX.Length), out var age))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new MinimumAgeRequirement(age));
                return Task.FromResult<AuthorizationPolicy?>(policy.Build());
            }
            return Task.FromResult<AuthorizationPolicy?>(null);
        }
    }

}
