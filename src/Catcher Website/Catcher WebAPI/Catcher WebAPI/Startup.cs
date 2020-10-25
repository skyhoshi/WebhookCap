using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Catcher_WebAPI.Data;
using Catcher_WebAPI.Data.CanvasData;
using Catcher_WebAPI.Data.CanvasLogging;
using Catcher_WebAPI.Data.Diagnostics.Interceptors;
using Catcher_WebAPI.Middleware;
using Catcher_WebAPI.Middleware.UserClaimsTransformations;
using Catcher_WebAPI.Models.Tables;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ApplicationDbContext = Catcher_WebAPI.Data.ApplicationDbContext;
using NoopClaimsTransformation = Microsoft.AspNetCore.Authentication.NoopClaimsTransformation;

namespace Catcher_WebAPI
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



            services.AddDbContext<ApplicationDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("CatchWatchUserManagement"))
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(true)
                .EnableServiceProviderCaching(false)
                .AddInterceptors(new IInterceptor[] { new dbCommandInterceptor(), new dbConnectionInterceptor(), new dbTransactionInterceptor() })
            );
            services.AddDbContext<GoCanvasLoggingDbContext>(options => options.
                UseSqlServer(Configuration.GetConnectionString("CatchWatchData"))
                .EnableDetailedErrors(true)
                .AddInterceptors(new IInterceptor[] { new dbCommandInterceptor(), new dbConnectionInterceptor(), new dbTransactionInterceptor() })
            );
            services.AddDbContext<GoCanvasDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("CatchWatchData"))
                .EnableDetailedErrors(true)
                .AddInterceptors(new IInterceptor[] { new dbCommandInterceptor(), new dbConnectionInterceptor(), new dbTransactionInterceptor() })
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            );
            IdentityOptions identityOptions = new IdentityOptions
            {
                Stores = {
                    MaxLengthForKeys = 50//Default:Max
                },
                SignIn = {
                    RequireConfirmedAccount = false//Default: false
                },
                User = {
                    RequireUniqueEmail = false//having this "RequireUniqueEmail" Set to true requires one to provide an email. this shouldn't be required for this system but can be made to be required at a later time.
                },
                Password = {
                    RequireDigit = false,//Default: True
                    RequireNonAlphanumeric = false,//Default : True
                    RequireUppercase = false,//Default: True
                    RequiredLength = 5//Default 6
                }
            };

            services
                .AddIdentity<tblUser,IdentityRole<string>>(options=>
                    {
                        options.Stores.MaxLengthForKeys = 50;//Default:Max
                        options.SignIn.RequireConfirmedAccount = false;//Default: false
                        options.User.RequireUniqueEmail = false; //having this "RequireUniqueEmail" Set to true requires one to provide an email. this shouldn't be required for this system but can be made to be required at a later time.
                        options.Password.RequireDigit = false;//Default: True
                        options.Password.RequireNonAlphanumeric = false;//Default : True
                        options.Password.RequireUppercase = false;//Default: True
                        options.Password.RequiredLength = 5; //Default 6
                    }
                )
                .AddRoleStore<RoleStore<IdentityRole<string>,ApplicationDbContext>>()
                .AddRoleManager<RoleManager<IdentityRole<string>>>()
                .AddRoleValidator<RoleValidator<IdentityRole<string>>>()
                .AddUserStore<UserStore<tblUser, IdentityRole<string>, ApplicationDbContext>>()
                .AddUserManager<UserManager<tblUser>>()
                .AddUserValidator<UserValidator<tblUser>>()
                .AddSignInManager<SignInManager<tblUser>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                ;


            services
                .AddMvc(options =>
                {
                    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
                    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                })
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddXmlDataContractSerializerFormatters()
                .AddXmlSerializerFormatters();

            services
                .AddControllers(options =>
                {
                    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
                    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                })
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddXmlDataContractSerializerFormatters()
                .AddXmlSerializerFormatters();

            services
                .AddControllersWithViews(options => { options.InputFormatters.Add(new XmlSerializerInputFormatter(options)); options.OutputFormatters.Add(new XmlSerializerOutputFormatter()); })
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddXmlDataContractSerializerFormatters()
                .AddXmlSerializerFormatters();

            services.AddAuthentication();

            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(NoopClaimsTransformation));

            services.Remove(serviceDescriptor);

            services.TryAddSingleton<IClaimsTransformation, CatchWatchAdministrationClaimsTransformation>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseRouting();

            app.UseRequestDatabaseLogger();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(name: "areas", "apitester", "{area:exists}/{controller=apitester}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            CreateRoles(services).Wait();
        }
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<string>>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<tblUser>>();

            IdentityResult roleResult;
            //here in this line we are adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("SuperUser");
            if (!roleCheck)
            {
                //here in this line we are creating admin role and seed it to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("SuperUser"));
            }
            //here we are assigning the Admin role to the User that we have registered above 
            //Now, we are assinging admin role to this user("Ali@gmail.com"). When will we run this project then it will
            //be assigned to that user.
            tblUser user = await UserManager.FindByEmailAsync("superuser@webhookcap.active");
            if (user != null)
            {

                await UserManager.AddToRoleAsync(user, "SuperUser");
            }
            else
            {
                var User = new tblUser();
                await UserManager.AddToRoleAsync(User, "SuperUser");
            }
            
            
        }
    }
}
