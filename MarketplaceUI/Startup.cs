using MarketplaceUI.Interfaces;
using MarketplaceUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MarketplaceUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var apiBaseUrl = Configuration["ApiSettings:BaseUrl"];

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<CustomAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            //services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddHttpClient<IUserService, UserService>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IPostService, PostService>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            });

            //services.AddHttpClient<IItemService, ItemService>(client =>
            //{
            //    client.BaseAddress = new Uri(apiBaseUrl);
            //});

            services.AddHttpClient<ITransactionService, TransactionService>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<ILoginService, LoginService>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddAuthorizationCore();
        }

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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}