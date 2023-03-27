using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using System.Text.Json.Serialization;
using PersonalFinancialManagement.Models.Entities.Identities;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos;
using PersonalFinancialManagement.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PersonalFinancialManagement.API.Infrastructures.ServicesExtensions
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();
                Enum.GetNames(context.Type)
                    .ToList()
                    .ForEach(name => schema.Enum.Add(new OpenApiString($"{Convert.ToInt64(Enum.Parse(context.Type, name))} = {name}")));
            }
        }
    }
    public static class GeneralServiceExtension
    {
        public static void AddGeneralConfigurations(this WebApplicationBuilder builder, string policyName, CorsOptions corsOption)
        {
            //builder.Services.AddSwaggerGen();
            builder.Services.AddCors(c =>
            {
                c.AddPolicy(policyName, options =>
                {
                    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    if (corsOption.AllowedOrigins.IsAllowedAll())
                    {
                        options.AllowAnyOrigin();
                    }
                    else
                    {
                        options.WithOrigins(corsOption.AllowedOrigins);
                    }

                    if (corsOption.AllowedMethods.IsAllowedAll())
                    {
                        options.AllowAnyMethod();
                    }
                    else
                    {
                        options.WithMethods(corsOption.AllowedMethods);
                    }

                    if (corsOption.ExposedHeaders.IsAllowedAll())
                    {
                        options.AllowAnyHeader();
                    }
                    else
                    {
                        options.WithHeaders(corsOption.ExposedHeaders);
                    }
                });
            });

            builder.Services.Configure<ConnectionString>(builder.Configuration.GetSection("ConnectionStrings"));
            var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionString>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.EnableDetailedErrors(true);

                options.UseSqlServer(connectionStrings.MigrationConnection);
                options.UseSnakeCaseNamingConvention();
            });
            builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));
            var jwtOptions = builder.Configuration.GetSection("JWT").Get<JwtOptions>();
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions?.ValidAudience,
                    ValidIssuer = jwtOptions?.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
                };
            });
            builder.Services.AddSwaggerGenNewtonsoftSupport();
            builder.Services.AddControllers()
                .AddNewtonsoftJson()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Example Token: 'Bearer {Token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oath2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
                c.SchemaFilter<EnumSchemaFilter>();
            });
        }
        private static bool IsAllowedAll(this string[] values)
        {
            return values == null || values.Length == 0 || values.Contains("*");
        }
    }
}
