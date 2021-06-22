using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Chechka.DAL.Data;             //Lb4.5.3 »спользование классов Identity в основном проекте
using Chechka.DAL.Entities;         //Lb4.5.3 »спользование классов Identity в основном проекте
using Chechka.Services;             //Lb4.5.6 —оздание класса инициализации базы данных
using Microsoft.AspNetCore.Mvc;

namespace Chechka
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDatabaseDeveloperPageExceptionFilter();

            //Lb4.5.6 {-
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            //Lb4.5.6 -}

            //Lb4.5.6 {»зменение настройки Identity, добавление возможности использовани€ 
            //класса ApplicationUser, ролей  и простых паролей
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            //Lb4.5.6 }

            ////Lb4. ƒл€ использовани€ Razor страниц
            services.AddRazorPages();

            //Lb8.4.5.3{–азрешаем использование сессии в проекте
            services.AddDistributedMemoryCache();
            services.AddSession(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.IsEssential = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //Lb8.4.5.3}

            ////Lb4. пути к созданным страницам в куки аутентификации
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env
            //Lb4.5.6 {¬ендрение классов ApplicationDbContext, UserManager и RoleManager
            ,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
            //Lb4.5.6 }
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            //Lb8.4.5.3{
            app.UseSession();
            //Lb8.4.5.3}

            app.UseAuthorization();

            //Lb4.5.6 {¬ызов метода заполн€ющего базу начальными данными класса инициализации
            DbInitializer.Seed(context, userManager, roleManager)
            .GetAwaiter()
            .GetResult();
            //Lb4.5.6 }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
