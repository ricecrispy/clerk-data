using clerk_data_data_access.Factory;
using clerk_data_data_access.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace clerk_data_service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();
            services.AddHttpClient();

            services.AddMvcCore().AddApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clerk Data Microservice API", Version = "v1"});
                string commentFilePath = Path.Combine(AppContext.BaseDirectory, "clerk-data-service.xml");
                c.IncludeXmlComments(commentFilePath);
            });

            services.Configure<PostgreSqlConnectionFactoryOptions>(Configuration.GetSection(nameof(PostgreSqlConnectionFactoryOptions)));
            services.AddScoped<IDbConnectionFactory, PostgreSqlConnectionFactory>();
            services.AddScoped<IMemberDataRepository, MemberDataRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<ICommitteeRepository, CommitteeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                c.RoutePrefix = "api";
            });
        }
    }
}
