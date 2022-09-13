using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MovieManager.Configurations.DependencyInjection;
using MovieManager.Helpers.SettingsModels;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

// Configurating Serilog 
var loggerConfiq = builder.Configuration.GetSection("Serilog");
builder.Host.UseSerilog((context, confiq) =>
{
    confiq.ReadFrom.ConfigurationSection(loggerConfiq);
});

// Configuring MovieManagerSettings section from appsettings.json
var appConfiq = builder.Configuration.GetSection("MovieManagerSettings");
var movieManagerSetting = appConfiq.Get<MovieManagerSettings>();
builder.Services.AddOptions<MovieManagerSettings>().Bind(appConfiq);
var secret = Encoding.ASCII.GetBytes(movieManagerSetting.MovieManagerSecret);

// Configuring Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secret),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        //RoleClaimType = "Admin",
    };
});


// Injecting Dependencies and Configurations
builder.Services.InjectDbContext(movieManagerSetting.MovieManagerDbConnection)
                .InjectRepositories()
                .InjectServices()
                .AddSwaggerConfiq()
                .InjectAutoMapper()
                .InjectFluentValidator();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();  // Logs every request

app.UseAuthentication();    // Use Authentication as well
app.UseAuthorization();

app.MapControllers();

app.Run();
