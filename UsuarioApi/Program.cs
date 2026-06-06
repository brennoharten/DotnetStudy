using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UsuarioApi.Authorization;
using UsuarioApi.Interfaces;
using UsuarioApi.Models;
using UsuarioApi.Repositories;
using UsuarioApi.Services;
using UsuarioApi.Services.Public;
using UsuarioApi.Infrastructure.Mongo;
using UsuarioApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});


var connectionString = builder.Configuration["ConnectionStrings:UsuarioConnection"];

builder.Services.AddDbContext<UsuarioDbContext>(opts =>
    opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ILandingPageService, LandingPageService>();
builder.Services.AddScoped<IPublicLandingPageRepository, PublicLandingPageRepository>();
builder.Services.AddScoped<IPublicLandingPageService, PublicLandingPageService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IdadeMinima", policy =>
    {
        policy.AddRequirements(new IdadeMinima(18));
    });
});

builder.Services.AddSingleton<IMapper>(sp =>
{
    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
    }, loggerFactory);

    return config.CreateMapper();
});

// MongoDB settings and context
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UsuarioApi v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
