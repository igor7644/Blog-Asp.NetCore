using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Email;
using Business.Commands;
using Business.Interfaces;
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
using Swashbuckle.AspNetCore.Swagger;

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

            var section = Configuration.GetSection("Email");
            var sender = new SmtpEmailSender(section["host"], Int32.Parse(section["port"]), section["fromAddress"], section["password"]);
            services.AddSingleton<IEmailSender>(sender);

            services.AddTransient<IGetCategoriesCommand, GetCategoriesCommand>();
            services.AddTransient<IGetCategoryCommand, GetCategoryCommand>();
            services.AddTransient<IAddCategoryCommand, AddCategoryCommand>();
            services.AddTransient<IEditCategoryCommand, EditCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, DeleteCategoryCommand>();

            services.AddTransient<IGetUsersCommand, GetUsersCommand>();
            services.AddTransient<IGetUserCommand, GetUserCommand>();
            services.AddTransient<IAddUserCommand, AddUserCommand>();
            services.AddTransient<IEditUserCommand, EditUserCommand>();
            services.AddTransient<IDeleteUserCommand, DeleteUserCommand>();

            services.AddTransient<IGetPostsCommand, GetPostsCommand>();
            services.AddTransient<IGetPostCommand, GetPostCommand>();
            services.AddTransient<IAddPostCommand, AddPostCommand>();
            services.AddTransient<IEditPostCommand, EditPostCommand>();
            services.AddTransient<IDeletePostCommand, DeletePostCommand>();

            services.AddTransient<IGetCommentsCommand, GetCommentsCommand>();
            services.AddTransient<IGetCommentCommand, GetCommentCommand>();
            services.AddTransient<IAddCommentCommand, AddCommentCommand>();
            services.AddTransient<IEditCommentCommand, EditCommentCommand>();
            services.AddTransient<IDeleteCommentCommand, DeleteCommentCommand>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Blog API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
