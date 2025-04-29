using InventoryTrackingSystem.EntityFrameworkCore;
using InventoryTrackingSystem.Repository;
using InventoryTrackingSystem.Managers.Products;
using InventoryTrackingSystem.Managers.Stores;
using InventoryTrackingSystem.Managers.StockMovements;
using InventoryTrackingSystem.Managers.StoreProductStocks;
using Microsoft.EntityFrameworkCore;
using InventoryTrackingSystem.Automapper;
using InventoryTrackingSystem.Managers.Users;
using InventoryTrackingSystem.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Managers and Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<IStoreManager, StoreManager>();
builder.Services.AddScoped<IStockMovementManager, StockMovementManager>();
builder.Services.AddScoped<IStoreProductStockManager, StoreProductStockManager>();
builder.Services.AddScoped<IUserManager, UserManager>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//Rate limiting
// Add rate-limiting services
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddControllers();

// Configure Swagger with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory API", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
            Array.Empty<string>()
        }
    });

    // Add the operation filter to handle [Authorize] attributes
    c.OperationFilter<AuthResponsesOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API v1");
        c.ConfigObject.AdditionalItems["oauth2RedirectUrl"] = "/swagger/oauth2-redirect.html";
    });
}

// CORRECT ORDER: Authentication BEFORE Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseIpRateLimiting(); // Add rate-limiting middleware

app.MapControllers();

app.Run();