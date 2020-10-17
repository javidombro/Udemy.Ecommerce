using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Udemy.Ecommerce.Application.Interface;
using Udemy.Ecommerce.Application.Main;
using Udemy.Ecommerce.Domain.Core;
using Udemy.Ecommerce.Domain.Interface;
using Udemy.Ecommerce.Infraestructure.Data;
using Udemy.Ecommerce.Infraestructure.Interface;
using Udemy.Ecommerce.Infraestructure.Repository;
using Udemy.Ecommerce.Service.WebAPI.Helpers;
using Udemy.Ecommerce.Transversal.Common;
using Udemy.Ecommerce.Transversal.Mapper;

namespace Udemy.Ecommerce.Service.WebAPI
{
    public class Startup
    {
        public readonly string myPolicy = "EcommerceApiPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(); });

            var appSettingsSection = Configuration.GetSection("Config");
            services.Configure<AppSettings>(appSettingsSection);

            #region Dependency Injection

            services.AddSingleton(Configuration);
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<ICustomerApplication, CustomerApplication>();
            services.AddScoped<ICustomerDomain, CustomerDomain>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<IUserDomain, UserDomain>();
            services.AddScoped<IUserRepository, UserRepository>();

            #endregion Dependency Injection

            #region Swagger

            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new OpenApiInfo()
               {
                   Version = "v1",
                   Title = "Ecommerce Application",
                   Contact = new OpenApiContact()
                   {
                       Email = "javi.dombro@gmail.com",
                       Name = "Javier Alejandro Dombronsky"
                   },
                   Description = "Curso de Udemy sobre Arquitectura de aplicaciones empresariales",
                   License = new OpenApiLicense()
                   {
                       Name = "GNU"
                   }
               });

               var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
               c.IncludeXmlComments(xmlPath);
           });

            #endregion Swagger

            #region CORS

            services.AddCors(options =>
           {
               options.AddPolicy(myPolicy,
                   builder =>
                   {
                       builder.WithOrigins(Configuration["Config:OriginCors"])
                       .AllowAnyHeader()
                       .AllowAnyMethod();
                   });
           });

            #endregion CORS
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Ecommerce v1");
            });

            app.UseCors(myPolicy);

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}