using Microsoft.EntityFrameworkCore;
using Serilog;
using Vizsgaremek.Data;
using Vizsgaremek.Repositories;
using Vizsgaremek.Repositories.Interfaces;
using Vizsgaremek.Services;
using Vizsgaremek.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Logging
//builder.Logging.ClearProviders();
builder.Logging.AddSerilog(new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger());

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// JWT bearer token authentication
builder.Services.AddAuthentication("Bearer")
   .AddJwtBearer("Bearer", opt =>
   {
       opt.RequireHttpsMetadata = false;
       opt.Authority = "https://localhost:5001";
       opt.Audience = "vizsgaremekAPI";
   });

builder.Services.AddControllers();

// EF DBContext
builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

// Repositories
builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IStorageRackRepository, StorageRackRepository>();
builder.Services.AddScoped<IShelfRepository, ShelfRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShelfProductRepository, ShelfProductRepository>();

// Services
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IStorageRackService, StorageRackService>();
builder.Services.AddScoped<IShelfService, ShelfService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShelfProductService, ShelfProductService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 443;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        DatabaseSeed.Initialize(services.GetRequiredService<DatabaseContext>());
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
