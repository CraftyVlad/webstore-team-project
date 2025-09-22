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
    pattern: "{controller=Products}/{action=GetProduct}/{id?}");

// Маршрути додавання
app.MapControllerRoute(
    name: "addProduct",
    pattern: "add-product",
    defaults: new { controller = "Products", action = "AddProduct" });

app.MapControllerRoute(
    name: "addReview",
    pattern: "add-review",
    defaults: new { controller = "Reviews", action = "AddReview" });

app.MapControllerRoute(
    name: "addComment",
    pattern: "add-comment",
    defaults: new { controller = "Comments", action = "AddComment" });

app.MapControllerRoute(
    name: "addCategory",
    pattern: "add-category",
    defaults: new { controller = "Categories", action = "AddCategory" });



await app.RunAsync();
