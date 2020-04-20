namespace FamilyKitchen.Web
{
    using System.Reflection;
    using Nest;
    using CloudinaryDotNet;
    using FamilyKitchen.Data;
    using FamilyKitchen.Data.Common;
    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Data.Repositories;
    using FamilyKitchen.Data.Seeding;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Services.Mapping;
    using FamilyKitchen.Services.Messaging;
    using FamilyKitchen.Web.ViewModels;

    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using FamilyKitchen.Web.MappingConfiguration;
    using FamilyKitchen.Web.ElasticSearchConf;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System;
    using FamilyKitchen.Web.ExtensionsConf.ClaimConf;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<FamilyKitchenUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddClaimsPrincipalFactory<MyUserClaimsPrincipalFactory>();

            services.AddAuthentication()
                //.AddGoogle(googleOptions =>
                // {
                //     googleOptions.ClientId = this.configuration["Authentication:Google:ClientId"];
                //     googleOptions.ClientSecret = this.configuration["Authentication:ClientSecret"];
                // })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = this.configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = this.configuration["Authentication:Facebook:AppSecret"];
                });

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(configure =>
            {
               configure.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddResponseCaching();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            services.AddSignalR(options => options.EnableDetailedErrors = true);

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services.AddRazorPages();
            services.AddAutoMapper(cfg => cfg.AddProfile<FamilyKitchenProfile>(), typeof(Startup));

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(FamilyKitchen.Data.Common.Repositories.IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IShopProductsService, ShopProductsService>();
            services.AddTransient<IFoodResourcesService, FoodResourcesService>();
            services.AddTransient<IRecipesService, RecipesService>();
            services.AddTransient<ISubCategoriesService, SubCategoriesService>();
            services.AddTransient<IShoppingCartsService, ShoppingCartsService>();
            services.AddTransient<IFavoriteProductService, FavoriteProductService>();
            services.AddTransient<IFamilyKitchenUsersService, FamilyKitchenUsersService>();

            Account account = new Account(
             this.configuration["Cloudinary:AppName"],
             this.configuration["Cloudinary:AppKey"],
             this.configuration["Cloudinary:AppSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);

            //services.AddSingleton<IProductService, ElasticSearchProductService>();
            //services.Configure<ProductSettings>(configuration.GetSection("shopProducts"));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddElasticsearch(this.configuration);
        }

        // mongodb+srv://PerseusBul:PerseusBul1@cluster0-aw8nr.azure.mongodb.net/test?retryWrites=true&w=majority // TODO encode

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new FamilyKitchenDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseResponseCompression();
            //app.UseResponseCaching();
            //app.UseHttpContextItemsMiddleware();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(
                endpoints =>
                    {
                        //endpoints.MapHub<MyHub>("/hubRoute");
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
