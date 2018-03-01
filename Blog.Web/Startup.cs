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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Blog.Web.Sitemap;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using Microsoft.Azure.Search;
using Blog.Domain.Command;

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
            services.AddScoped<GetPostsFromSearchQuery>();

            services.AddScoped<AddPostCommand>();
            services.AddScoped<EditPostCommand>();

            services.AddScoped<ISearchIndexClient>((serviceProvider) =>
            {
                var searchServiceClient = new SearchServiceClient(Configuration["Data:AzureSearch:Name"], new SearchCredentials(Configuration["Data:AzureSearch:Key"]));

                return searchServiceClient.Indexes.GetClient(Configuration["Data:AzureSearch:IndexName"]);
            });

            services.AddScoped<SitemapBuilder>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddMvc();

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddApplicationInsightsTelemetry(Configuration);
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
                app.UseExceptionHandler("/oops");
            }

            var options = new RewriteOptions().AddRedirectToHttps();
            app.UseRewriter(options);

            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("/oops/{0}");

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
