using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamersUnited.Core.ApplicationService;
using GamersUnited.Core.ApplicationService.Impl;
using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data;
using GamersUnited.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GamersUnited.RestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IRepository<Game>, GameRepository>();
            services.AddScoped<IRepository<GameGenre>, GameGenreRepository>();
            services.AddScoped<IRepository<Invoice>, InvoiceRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<ProductCategory>, ProductCategoryRepository>();
            services.AddScoped<IRepository<Sold>, SoldRepository>();
            services.AddScoped<IRepository<Stock>, StockRepository>();
            services.AddScoped<IRemoveStockWithProduct, StockRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<ILoginValidation, UserRepository>();

            if (Environment.IsDevelopment())
            {
                // In-memory database:
                services.AddDbContext<GamersUnitedContext>(opt => opt.UseInMemoryDatabase("InMemory: GamersUnitedContext").EnableSensitiveDataLogging());
            }
            else
            {
                // SQL Server on Azure:
                services.AddDbContext<GamersUnitedContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            }

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add CORS
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<GamersUnitedContext>();
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();

                    app.UseCors(builder => builder.WithOrigins("https://localhost:63342").AllowAnyMethod());
                }
                else
                {
                    app.UseHsts();

                    app.UseCors(builder => builder.WithOrigins("https://gamersunited.azurewebsites.net").AllowAnyMethod().AllowAnyHeader());
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
