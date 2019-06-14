using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Commands;
using Commands;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<Context>();
            services.AddTransient<IGetCategoriesCommand, GetCategoriesCommand>();
            services.AddTransient<IGetCategoryCommand, GetCategoryCommand>();
            services.AddTransient<IGetUsersCommand, GetUsersCommand>();
            services.AddTransient<IGetUserCommand, GetUserCommand>();
            services.AddTransient<IGetPostsCommand, GetPostsCommand>();
            services.AddTransient<IGetPostCommand, GetPostCommand>();
            services.AddTransient<IGetCommentsCommand, GetCommentsCommand>();
            services.AddTransient<IGetCommentCommand, GetCommentCommand>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
