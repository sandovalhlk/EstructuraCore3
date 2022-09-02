using AppGlobal.Core.Interfaces;
using AppGlobal.Core.Services;
using AppGlobal.Infrastructure.Data;
using AppGlobal.Infrastructure.Filters;
using AppGlobal.Infrastructure.Interfaces;
using AppGlobal.Core.CustomEntities;
using AppGlobal.Infrastructure.Repositories;
using AppGlobal.Infrastructure.Services;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            })
                .AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            })
            .ConfigureApiBehaviorOptions(options =>
                 {
                     options.SuppressModelStateInvalidFilter = true;
                 });

            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));
            services.AddDbContext<SocialMediaContext>(option => option.UseSqlServer(Configuration.GetConnectionString("SocialMedia")));

            services.AddTransient<IPostService, PostService>();

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://",request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });

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
