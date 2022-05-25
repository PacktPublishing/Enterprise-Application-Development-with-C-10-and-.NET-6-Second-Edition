using Microsoft.AspNetCore.Authorization;

namespace AuthSample.Core
{
    public class MinimumAgeRequirement: IAuthorizationRequirement
    {
        public int MinimumAge { get; set; }
        public MinimumAgeRequirement(int minimumAge)
        {
            this.MinimumAge = minimumAge;
        }
    }
}
