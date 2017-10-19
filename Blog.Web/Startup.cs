using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blog.Data;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Blog.Domain.Queries;
using Blog.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Blog.Web.Sitemap;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Blog.Web
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
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<BlogContext>(options => options.UseSqlServer(Configuration["Data:BlogConnection:ConnectionString"]));
            services.AddScoped<IBlogContext>(provider => provider.GetService<BlogContext>());

            services.AddScoped<QueryCommandBuilder>();
            services.AddScoped<GetCategoriesWithPostsNumberQuery>();
            services.AddScoped<GetDraftQuery>();
            services.AddScoped<GetPostQuery>();
            services.AddScoped<GetPostsQuery>();
            services.AddScoped<GetCategoriesQuery>();

            services.AddScoped<SitemapBuilder>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                SupportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("fr"),
                },
                DefaultRequestCulture = new RequestCulture("fr")
            });

            app.UseMvc();
        }
    }
}
