using Microsoft.AspNetCore.Authorization;

namespace AuthSample.Core
{
    public class AuthorizeAgeOverAttribute : AuthorizeAttribute
    {
        const string PREFIX = "Over";
        public AuthorizeAgeOverAttribute(int age) => Age = age;
        public int Age
        {
            get
            {
                if (int.TryParse(Policy?.Substring(PREFIX.Length), out var age))
                {
                    return age;
                }
                return default(int);
            }
            set
            {
                Policy = $"{PREFIX}{value.ToString()}";
            }
        }
    }

}
