using CatApi.Configurations;
using CatApi.Repositories;
using CatApi.Services;
using Microsoft.Win32;

var builder = WebApplication.CreateBuilder(args);

var apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();

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


// Configure CORS using the allowed origins from configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(apiSettings.AllowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Make sure to apply the CORS policy early in the middleware pipeline.
app.UseCors("AllowSpecificOrigins");

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
