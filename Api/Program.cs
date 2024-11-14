#region Using
using System.Text;
using Api.Hubs;
using Api.Middlewares;
using Application.Commands.Bid.CreateBid;
using Application.Commands.Tender.CreateTender;
using Application.Interfaces.Bid;
using Application.Interfaces.Tender;
using Application.Interfaces.User;
using Application.Queries.Tender.GetAllTenders;
using Application.Queries.Tender.GetInProcessTendersWithDetails;
using Application.Services.Bid;
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
#endregion


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();
//    .AddNewtonsoftJson(options =>
//{
//    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
//});
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
// command & query handlers
builder.Services.AddScoped<IGetAllTendersQueryHandler,GetAllTendersQueryHandler>();
builder.Services.AddScoped<IGetInProcessTendersWithDetailsHandler,GetInProcessTendersWithDetailsHandler>();
builder.Services.AddScoped<ICreateTenderCommandHandler, CreateTenderCommandHandler>();
builder.Services.AddScoped<ICreateBidCommandHandler, CreateBidCommandHandler>();
// services
builder.Services.AddScoped<IBidCommandService,BidCommandService>();
builder.Services.AddScoped<ITenderCommandService, TenderCommandService>();
builder.Services.AddScoped<ITenderQueryService, TenderQueryService>();
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
app.MapHub<NotificationHub>("/notificationhub");

app.Run();