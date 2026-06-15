using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reactivities.API;
using Reactivities.Application;
using Reactivities.Identity;
using Reactivities.Identity.Models;
using Reactivities.Identity.Persistence;
using Reactivities.Infrastructure;
using Reactivities.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddApiServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Identity
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<IdentityAppDbContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins(["http://localhost:5173", "https://localhost:5173"]));
});

var app = builder.Build();

app.UseExceptionHandler();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

// Identity Api
app.MapGroup("api").MapIdentityApi<ApplicationUser>();

app.MapControllers();

using (var scoped = app.Services.CreateScope())
{
    var service = scoped.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = service.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await AppDbContextSeed.SeedAsync(context, loggerFactory);

        var identityContext = service.GetRequiredService<IdentityAppDbContext>();
        await identityContext.Database.MigrateAsync();
        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
        await IdentityAppDbContextSeed.SeedAsync(userManager, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Migration error");
    }
}

app.Run();
