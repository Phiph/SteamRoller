using ElectronNET.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SteamRoller.API.Client;
using SteamRoller.Client.Mappings;
using SteamRoller.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamRoller.Client
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
            services.AddHttpContextAccessor();
            services.AddSingleton<IAuthenticatedHttpClient, AuthenticatedHttpClient>();
            // NSwag generated SDK client
 
            services.AddSingleton<ISteamLibraryService,SteamLibraryService>();
           
            services.AddTransient<IPlayerClient, PlayerClient>();
            services.AddTransient<IRoomClient, RoomClient>();
            services.AddTransient<ISteamRollerService, SteamRollerService>();




            services.AddRazorPages().AddSessionStateTempDataProvider();

            services.AddAutoMapper(typeof(SteamLibraryProfile));

            services.AddSession();


            

          
            services.AddElectron();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            // Open the Electron-Window here
            Task.Run(async () => {
                await Electron.WindowManager.CreateWindowAsync();
                await Electron.App.SetAsDefaultProtocolClientAsync("steamroller");
                });
        }
    }
}
