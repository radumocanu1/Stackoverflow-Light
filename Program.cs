using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Stackoverflow_Light.Configurations;
using Stackoverflow_Light.Exceptions;
using Stackoverflow_Light.Repositories;
using Stackoverflow_Light.Services;
using Stackoverflow_Light.Utils;

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
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 3, 0))));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<TokenClaimsExtractor>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();


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
