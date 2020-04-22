using Microsoft . AspNetCore . Builder;
using Microsoft . AspNetCore . Hosting;
using Microsoft . EntityFrameworkCore;
using Microsoft . Extensions . Configuration;
using Microsoft . Extensions . DependencyInjection;
using Microsoft . Extensions . Hosting;

using SalesTax . DataAccess . Models . TaxJar;

namespace SalesTax
    {
    public class Startup
        {
        private IConfiguration _config;
        public Startup ( IConfiguration config )
            {
            _config = config;
            }

        public IConfiguration Configuration
            {
            get;
            }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices ( IServiceCollection services )
            {
            services . AddDbContextPool<AppDbContext> ( options => options . UseSqlServer (_config .GetConnectionString( "ItemReqDbConnection" ) ));
            services . AddScoped<IProductsReqRepo , MockReqRepo> ( );
            services . AddControllersWithViews ( );
            services . AddRazorPages ( );
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure ( IApplicationBuilder app , IWebHostEnvironment env )
            {
            if ( env . IsDevelopment ( ) )
                {
                app . UseDeveloperExceptionPage ( );
                }
            else
                {
                app . UseExceptionHandler ( "/Home/Error" );
                 }
            app . UseHttpsRedirection ( );
            app . UseStaticFiles ( );
            app . UseRouting ( );
            app . UseAuthorization ( );

            app . UseEndpoints ( endpoints =>
                {
                endpoints . MapControllerRoute (
                    name: "default" ,
                    pattern: "{controller=Home}/{action=Index}/{id?}" );

                endpoints . MapControllerRoute (
                    name: "listItems" ,
                    pattern: "{controller=Home}/{action=listItems}" );

                endpoints . MapControllerRoute (
                     name: "Index" ,
                    pattern: "{controller=Home}/{action=Index()}" );

                    endpoints . MapControllerRoute (
                    name: "gettax" ,
                    pattern: "{controller=Home}/{action=gettax}" );
                } );
            }
        }
    }
