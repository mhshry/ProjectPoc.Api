using Microsoft.Extensions.DependencyInjection;

namespace ProjectPoc.Business
{
    public static class RegisterHandler
    {
        public static IServiceCollection AddRequestHandlers(this IServiceCollection services)
        {
            return services.AddMediatR(r => r.RegisterServicesFromAssembly(typeof(RegisterHandler).Assembly));
        }
    }
}
