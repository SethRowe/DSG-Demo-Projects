using DSG.Sample.NetCore.BaseApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using NFS = DSG.Sample.NetFourSix.Api.Controllers;

namespace DSG.Sample.NetCore.Wrapper
{
    public class Startup
    {
        private const string SwaggerJsonUrl = "/swagger/v1/swagger.json";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<NFS.IValuesController, NFS.ValuesController>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.ConfigureBaseApi();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Error handling goes first, in case another middleware errors out
            app.UseErrorHandling();

            // TODO: See if this MapWhen is needed, or if Swagger can 'play nice' with the rest of the BaseAPI

            app.MapWhen(SwaggerRequested, a =>
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c => { c.SwaggerEndpoint(SwaggerJsonUrl, "My API V1"); });
            });

            app.MapWhen(SwaggerNotRequested, a =>
            {
                // Add in the BaseApi middleware
                UseBaseApiMiddleware(a);
                // Add in our controllers
                a.UseMvc();
            });
        }

        private bool SwaggerRequested(HttpContext context)
        {
            var requestPath = context.Request.Path.Value.ToLower();

            return requestPath == "/swagger" || requestPath == SwaggerJsonUrl;
        }

        private bool SwaggerNotRequested(HttpContext context)
        {
            return !SwaggerRequested(context);
        }

        private void UseBaseApiMiddleware(IApplicationBuilder app)
        {
            app.UseEnableRequestRewind();
            app.UseRequestProperties();
            app.UsePartnerBasedFiltering();
            app.UseFailureResultRewriting();
        }
    }
}
