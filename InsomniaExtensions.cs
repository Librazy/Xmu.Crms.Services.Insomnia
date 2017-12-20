using Xmu.Crms.Services.Insomnia;
using Xmu.Crms.Shared.Service;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InsomniaExtensions
    {
        public static IServiceCollection AddInsomniaUserService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<IUserService, UserService>();
        }

        public static IServiceCollection AddInsomniaTimerService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<ITimerService, TimerService>();
        }
    }
}
