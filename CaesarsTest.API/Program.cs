using CaesarsTest.API.DbContexts;
using CaesarsTest.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GuestContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddMemoryCache();

builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(setupAction =>
//{
//    setupAction.AddSecurityDefinition("GuestApiBearerAuth", new OpenApiSecurityScheme()
//    {
//        Type = SecuritySchemeType.Http,
//        Scheme = "Bearer",
//        Description = "Input a valid token to access this API"
//    });

//    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "GuestApiBearerAuth" }
//            }, new List<string>() }
//    });
//});

builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IGuestCacheService, GuestCacheService>();
builder.Services.AddScoped<IGuestService, GuestService>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

var roleClaim = builder.Configuration["JwtOptions:RoleClaim"];
if (string.IsNullOrWhiteSpace(roleClaim)) roleClaim = ClaimTypes.Role;

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeAnAdmin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim(roleClaim, "admin");
    });
});

builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    setupAction.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
