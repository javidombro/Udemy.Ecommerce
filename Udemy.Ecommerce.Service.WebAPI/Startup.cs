using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
            //services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            var appSettingsSection = Configuration.GetSection("Config");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var issuer = appSettings.Issuer;
            var audience = appSettings.Audience;
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var userId = int.Parse(context.Principal.Identity.Name);
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

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

               //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
               //c.IncludeXmlComments(xmlPath);

               c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
               {
                   Description = "Authorization by API key",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.ApiKey,
                   Name = "Authorization"
               });

               c.AddSecurityRequirement(new OpenApiSecurityRequirement
               {
                   {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           }
                       },
                       new string[]{ }
                   }
               });
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Ecommerce v1");
            });

            app.UseCors(myPolicy);

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}