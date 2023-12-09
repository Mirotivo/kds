using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connString = builder.Configuration.GetConnectionString("Sqlite") ?? "";
// builder.Services.AddDbContext<DBContext>(option => option.UseSqlServer(connString));
builder.Services.AddDbContext<DBContext>(option => option.UseSqlite(connString));
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSignalR();
builder.Services.AddTransient<IStationGroupService, StationGroupService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IPurchaseOrderService, PurchaseOrderService>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DBContext>();
    DbInitializer.Initialize(dbContext);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// app.MapGet("/", () => "Hello World!");
app.MapHub<StationCommunicationHub>("/communication");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
