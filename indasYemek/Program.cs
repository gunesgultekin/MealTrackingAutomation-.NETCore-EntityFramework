using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using indasYemek.context;
using indasYemek.yemekListesiRepository1;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using indasYemek.Repositories;
using AspNet.Security.OAuth.Validation;

namespace Library.WebApi
{
    public class ErrorModel
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
    public class ErrorResponse
    {
        public List<ErrorModel> Error { get; set; } = new List<ErrorModel>();
        public bool Successful { get; set; }
    }
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray());

                var errorResponse = new ErrorResponse();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorModel = new ErrorModel
                        {
                            FieldName = error.Key,
                            Message = subError
                        };

                        errorResponse.Error.Add(errorModel);
                    }

                    context.Result = new BadRequestObjectResult(errorResponse);
                    return;
                }

                await next();


            }
        }
    }
    public class Program
    {
        IConfiguration Configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
             options.AddDefaultPolicy(builder =>
             builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            services.AddMvc();
            services.AddControllers(option =>
            {
                option.Filters.Add<ValidationFilter>();
            });


            services.AddAuthentication()

 .AddJwtBearer("JwtBearer", jwtBearerOptions =>
 {
     jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SECRET_KEY_12345678910")),
         ValidateIssuer = false,
         ValidateAudience = false,
         ValidateLifetime = false,
     };
  });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static void Main(string[] args)
        {
            IServiceCollection services;
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<yemekListesiRepository>();
            builder.Services.AddTransient<istekListesiRepository>();
            builder.Services.AddTransient<deviceTableRepository>();
            builder.Services.AddTransient<userTableRepository>();
            builder.Services.AddAuthentication()
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
        {
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SECRET_KEY_12345678910")),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
        };
        });
            builder.Services.AddMvc();
            builder.Services.AddCors(options =>
            options.AddDefaultPolicy(builder =>
            builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                });
            });
            builder.Services.AddDbContext<DBContext>(o =>
            {
                o.UseSqlServer("SQL SERVER CONNECTION STRING");
                
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.MapControllers();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run();
        }
    }
}