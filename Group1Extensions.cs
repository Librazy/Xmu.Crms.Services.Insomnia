using Xmu.Crms.Services.Group1;
using Xmu.Crms.Shared.Service;

namespace Microsoft.Extensions.DependencyInjection
{ 
    public static class Group1Extensions
    {
        public static IServiceCollection AddGroup1UserService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<IUserService, UserService>();
        }
    }
}
