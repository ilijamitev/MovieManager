using MovieManager.Configurations.DependencyInjection;
using MovieManager.Helpers.SettingsModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

// Configuring MovieManagerSettings section from appsettings.json
var appConfiq = builder.Configuration.GetSection("MovieManagerSettings");
var movieManagerSetting = appConfiq.Get<MovieManagerSettings>();
builder.Services.AddOptions<MovieManagerSettings>().Bind(appConfiq);

// Injecting Dependencies
builder.Services.InjectDbContext(movieManagerSetting.MovieManagerDbConnection)
                .InjectAutoMapper();












var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
