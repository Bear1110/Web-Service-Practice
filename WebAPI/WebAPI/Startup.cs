using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebAPI.Hubs;

namespace WebAPI
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
            //opt.UseInMemoryDatabase("Player")
            services.AddDbContext<GameDBContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")) );

            services.AddScoped<GameCenter, GameCenter>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>(); //For getting request ip info.
            services.AddControllers();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GameDBContext playerContext)
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
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapControllers();
            });

            playerContext.Database.EnsureCreated(); //create table if not exsit.
        }
    }
}
