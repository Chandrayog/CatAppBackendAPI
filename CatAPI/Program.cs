using CatApi.Configurations;
using CatApi.Repositories;
using CatApi.Services;
using Microsoft.Win32;

var builder = WebApplication.CreateBuilder(args);

// Read the URL from configuration
var backendUrl = builder.Configuration["BackendAPISettings:Url"];

if (!string.IsNullOrWhiteSpace(backendUrl))
{
    // This will override the URL specified in launchsettings.json for the current host
    builder.WebHost.UseUrls(backendUrl);
}


// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register configuration from appsettings.json and environment-specific files.
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Register HttpClient and Controllers.
builder.Services.AddHttpClient();
builder.Services.AddControllers();

// Register repository and service for dependency injection.
builder.Services.AddScoped<ICatRepository, CatRepository>();
builder.Services.AddScoped<ICatService, CatService>();

// Retrieve ApiSettings to configure CORS.
var apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins(apiSettings.FrontendUrl)
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
