using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;
using Project.Web2.Models;

namespace Project.Web2 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwagger();
            services.AddSignalR();

            services.AddScoped<IShoppingCartService, ShoppingCartService>();

            services
                .AddSingleton<IAuthorizationHandler, TerminalAppAuthorizationHandler
                >();
            services.AddAuthorization(options => {
                options.AddPolicy("TerminalApp",
                    policyBuilder => {
                        policyBuilder.Requirements.Add(
                            new TerminalAppAuthorizationRequirement());
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();


            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwaggerUi3WithApiExplorer(settings => {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });

            app.UseSignalR(routes => { routes.MapHub<ChatHub>("/chatHub"); });

            app.UseMvc();
        }
    }
}