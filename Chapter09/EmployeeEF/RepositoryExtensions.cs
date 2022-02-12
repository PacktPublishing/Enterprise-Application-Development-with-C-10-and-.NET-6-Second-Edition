using Microsoft.Extensions.DependencyInjection;

namespace EmployeeEF
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection
        AddRepositories(this IServiceCollection services)
        {
            return services;
        }
    }
}
