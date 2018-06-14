using Microsoft.Extensions.DependencyInjection;

namespace DSG.Sample.NetCore.BaseApi.Middleware
{
    public static class RegistrationExtensions
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void ConfigureBaseApi(this IServiceCollection services)
        {
            services.AddScoped<RequestProperties>();
        }
    }
}