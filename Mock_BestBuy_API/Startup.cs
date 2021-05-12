using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System.Data;

namespace Mock_BestBuy_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IDbConnection>(s =>
                {
                    IDbConnection conn = new MySqlConnection(Configuration.GetConnectionString("bestbuy"));
                    conn.Open();
                    return conn;
                });
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddCors(options =>
                {
                    options.AddPolicy("AllowOrigin", builder =>
                        {
                            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                        });
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowedOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
