using AppGlobal.Core.Interfaces;
using AppGlobal.Infrastructure.Data;
using AppGlobal.Infrastructure.Filters;
using AppGlobal.Infrastructure.Repositories;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AppGlobal.Api
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //funcion para no validar el modelo con el api controler
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options=> {
                    options.SuppressModelStateInvalidFilter = true;
                });

            services.AddDbContext<SocialMediaContext>(option => option.UseSqlServer(Configuration.GetConnectionString("SocialMedia")));

            services.AddTransient<IPostRepository, PostRepository>();

            services.AddMvc(options =>
            { //validacion global de los modelos q se ejecuten
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options =>
            {//validator registros
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });


        }

       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
