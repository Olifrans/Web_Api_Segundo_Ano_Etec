using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApiOlifrans.Data;

namespace WebApiOlifrans
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiOlifrans", Version = "v1" });
            });

            services.AddDbContext<WebApiOlifransContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("WebApiOlifransContext")));


            services.AddCors(options =>
            {
                options.AddPolicy(name: "EtecAcesso",
                    builder =>
                    {
                        builder.WithOrigins("http://127.0.0.1:5500")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });



            //// Configuração do CORS
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowedOrigins",
            //        policy =>
            //        {
            //            policy.WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>())
            //                   .AllowAnyHeader()
            //                   .AllowAnyMethod();
            //        });
            //});


            //teste -ok
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "AllowedOrigins",
            //                      policy =>
            //                      {
            //                          policy.WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>())
            //                               .AllowAnyHeader()
            //                                .AllowAnyMethod();
            //                      });
            //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiOlifrans v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
                        
            app.UseCors("EtecAcesso");

            //app.UseCors("AllowedOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
