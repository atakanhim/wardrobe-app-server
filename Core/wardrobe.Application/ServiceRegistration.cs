using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace wardrobe.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceRegistration)); // bu sınıfın bulundugu assemlbdeki tüm , ihandler , irequest sınıflarını bul ve aracı ol
            services.AddHttpClient();
        }
    }
}
