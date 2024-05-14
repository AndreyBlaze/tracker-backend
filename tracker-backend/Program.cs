
using Infrastructure.Persistence;
using Application;
using Infrastructure;
using Presentation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IO.Compression;
using System.Text;

namespace tracker_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
                            builder =>
                            {
                                builder.AllowAnyHeader()
                                       .AllowAnyMethod()
                                       .SetIsOriginAllowed((host) => true)
                                       .AllowCredentials();
                            }));

            builder.Services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.BufferBodyLengthLimit = int.MaxValue;
                x.MultipartBoundaryLengthLimit = int.MaxValue;
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
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
                            new List<string>()
                    }
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Issuer"],
                    ValidAudience = builder.Configuration["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SigningKey"] ?? ""))
                };
            });

            builder.Services
                .AddApplication()
                .AddInfrastructure()
                .AddPresentation();

            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.DependencyInjection).Assembly);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddResponseCaching();

            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });
            builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            builder.Services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.SmallestSize;
            });

            builder.Services.AddHttpContextAccessor();

            #region Database access configuration

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionTest");
#if DEBUG
            connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=tracker;";
#endif

            Console.WriteLine(connectionString);

            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(connectionString,
                o => o.SetPostgresVersion(12, 13)));
            #endregion

            var app = builder.Build();

            app.UseCors("CorsPolicy");

            app.UseResponseCaching();

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                }
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseWebSockets();
            app.UseAuthorization();

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()?
                    .Error;
                var response = new { error = exception?.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.MapControllers();

            app.Run();
        }
    }
}