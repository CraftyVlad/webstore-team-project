using Microsoft.EntityFrameworkCore;
using web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DB registering
builder.Services.AddDbContext<ApplicationContext>((options) => 
{
    var connection = builder.Configuration.GetConnectionString("SqlLiteConnection");
    options.UseSqlite(connection);
});

var app = builder.Build();

// DB Initializing
using (var scope = app.Services.CreateScope())
{
    var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

    await DbInitializer.Init(applicationContext);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=GetProducts}/{id?}");

await app.RunAsync();
