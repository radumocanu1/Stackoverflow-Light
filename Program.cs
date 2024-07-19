using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Stackoverflow_Light.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

// Add services relying on OIDC (using Keycloak as provider) 
KeycloakConfiguration.Initialize(builder.Configuration);
builder.Services.AddSwaggerWithKeycloak();
builder.Services.AddKeycloakAuthentication();
builder.Services.AddKeycloakAuthorization();

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 3, 0))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.OAuthClientId("LightClientID"); 
        c.OAuthAppName("My API - Swagger");
        c.OAuthUsePkce(); 
        c.OAuthScopes(new[] { "openid"});
    });
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
