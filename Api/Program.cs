using System.Text;
using Api.Middlewares;
using Application.Interfaces.Tender;
using Application.Interfaces.User;
using Application.Services.Tender;
using Application.Services.User;
using Domain.Models.User;
using Domain.Repositories;
using InfraStructure.Configuration.ErrorDescriber;
using InfraStructure.Data;
using InfraStructure.Repositories;
using InfraStructure.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region EF Core
builder.Services.AddDbContext<TenderContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TenderConnection")));
#endregion

#region Identity
builder.Services.AddIdentity<TenderUser, IdentityRole>()
    .AddEntityFrameworkStores<TenderContext>()
    //.AddUserValidator<UserValidator>()
    .AddErrorDescriber<CustomIdentityErrorDescriber>();
#endregion

#region Authorization
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    config.AddPolicy("Contractor", policy => policy.RequireRole("Contractor"));
});
#endregion

#region IOC
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
// Repositories
builder.Services.AddScoped<ITenderRepository, TenderRepository>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
// Services
builder.Services.AddScoped<ITenderQueryService, TenderQueryService>();
builder.Services.AddScoped<ITenderCommandService, TenderCommandService>();


#endregion

#region JWT



var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];
var key = Encoding.ASCII.GetBytes(secretKey!);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"]
        };
    });

#endregion

#region SwaggerJWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
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
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
