using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer.Data;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers();

// EF DBContext
builder.Services.AddDbContext<IdentityContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

// IdentityServer4
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
}).AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = dbBuilder =>
                        dbBuilder.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"),
                        sql => sql.MigrationsAssembly("IdentityServer"));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = dbBuilder =>
                        dbBuilder.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"),
                        sql => sql.MigrationsAssembly("IdentityServer"));
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 3600;
                })
                .AddDeveloperSigningCredential();
                //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                //.AddInMemoryApiScopes(Config.GetApiScopes())
                //.AddInMemoryApiResources(Config.GetApiResources())
                //.AddInMemoryClients(Config.GetClients())
                //.AddTestUsers(Config.GetUsers());

builder.Services.AddControllersWithViews();
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 443;
});

var app = builder.Build();

app.UseCors("AllowAll");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var identityContext = services.GetRequiredService<IdentityContext>();
        var configurationDbContext = services.GetRequiredService<ConfigurationDbContext>();
        var persistedGrantDbContext = services.GetRequiredService<PersistedGrantDbContext>();
        DatabaseSeed.Initialize(identityContext, configurationDbContext, persistedGrantDbContext, app);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.UseHttpsRedirection();

// Serve static files from wwwroot
app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.MapControllers();

app.Run();
