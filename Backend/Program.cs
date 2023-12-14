using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configurations
var key = builder.Configuration.GetValue<string>("Jwt:Key");

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddTransient<CustomMiddleware>();
builder.Services.AddSingleton<Dictionary<string, string>>(new Dictionary<string, string>());
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<kdsDbContext>(options =>
// {
//     // Ensure the database is created
//     options.UseSqlServer(connectionString);
//     options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//     options.EnableSensitiveDataLogging();
// });
string connString = builder.Configuration.GetConnectionString("Sqlite") ?? "";
builder.Services.AddDbContext<kdsDbContext>(option => option.UseSqlite(connString));
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddTransient<IStationGroupService, StationGroupService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IPurchaseOrderService, PurchaseOrderService>();
builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AutoMapper and the mapping configuration
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<kdsDbContext>();
    DbInitializer.Initialize(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting(); // Add this line to configure routing

app.UseCors(options =>
{
    options.WithOrigins(
        "https://localhost:8000",
        "https://localhost:9000/",
        "https://92.205.162.126:8000"
    );
    // options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.AllowCredentials();
});

app.UseAuthorization();
app.UseMiddleware<CustomMiddleware>();
app.UseAuthentication(); // Add authentication middleware

// app.MapControllers();
// app.MapGet("/", () => "Hello World!");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<StationCommunicationHub>("/communication");
    endpoints.MapHub<WebRtcHub>("/webrtc");
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Welcome to the dev page!");
    });
});

app.Run();