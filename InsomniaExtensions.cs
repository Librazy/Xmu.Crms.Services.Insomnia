using Xmu.Crms.Services.Insomnia;
using Xmu.Crms.Shared.Service;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InsomniaExtensions
    {
        public static IServiceCollection AddInsomniaSeminarGroupService(this IServiceCollection serviceCollection) =>
            serviceCollection.AddSingleton<ISeminarGroupService, GroupService>();

        public static IServiceCollection AddInsomniaFixedGroupService(this IServiceCollection serviceCollection) =>
    serviceCollection.AddSingleton<IFixGroupService, FixedGroupService>();
    }
}