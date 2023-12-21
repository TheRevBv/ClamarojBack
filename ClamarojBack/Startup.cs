//Startup.cs
using ClamarojBack.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ClamarojBack
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddHttpContextAccessor();
            //=====Cors Rules
            var misReglasCors = "ReglasCorsAngular";
            _ = services.AddCors(options =>
            {
                string[] corsUrls = Configuration.GetSection("Cors").GetSection("Urls").Get<string[]>()!;
                options.AddPolicy(misReglasCors,
                    builder =>
                    {
                        builder.WithOrigins(corsUrls)
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });

                options.AddPolicy("AllowFlutterApp",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                    });
            });

            // Add services to the container, en este caso los dbContext
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Clamaroj API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Ingresa un token valido",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                var secretKey = Configuration.GetSection("JWT").GetSection("Key").ToString();
                var keyInBytes = Encoding.UTF8.GetBytes(secretKey!);
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyInBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime, IWebHostEnvironment env)
        {
            Console.WriteLine("COnfigure inicializado");

            //if (env.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowFlutterApp");
            app.UseCors("ReglasCorsAngular");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //var seeder = new Seeders(app.ApplicationServices.GetRequiredService<AppDbContext>());
            //seeder.Seed();
        }
    }
}
