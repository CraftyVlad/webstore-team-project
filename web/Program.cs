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

// маршрути за атрибутами
app.MapControllers();

// прописані маршрути, закоментовані
{
    /*
    // Маршрути додавання
    app.MapControllerRoute(
        name: "addProduct",
        pattern: "product/add",
        defaults: new { controller = "Products", action = "AddProduct" });

    app.MapControllerRoute(
        name: "addReview",
        pattern: "review/add",
        defaults: new { controller = "Reviews", action = "AddReview" });

    app.MapControllerRoute(
        name: "addComment",
        pattern: "comment/add",
        defaults: new { controller = "Comments", action = "AddComment" });

    app.MapControllerRoute(
        name: "addCategory",
        pattern: "category/add",
        defaults: new { controller = "Categories", action = "AddCategory" });
    // Маршрути зміни
    app.MapControllerRoute(
        name: "updateProduct",
        pattern: "product/update/{id}",

        // у дефолтному маршруті прописати айді як {id}, не можна, лише явно вказати його

        defaults: new { controller = "Products", action = "UpdateProduct", id = 0 });

    app.MapControllerRoute(
        name: "updateReview",
        pattern: "review/update/{id}",
        defaults: new { controller = "Reviews", action = "UpdateReview", id = 0 });

    app.MapControllerRoute(
        name: "updateCategory",
        pattern: "category/update/{id}",
        defaults: new { controller = "Categories", action = "UpdateCategory", id = 0 });
    // Маршрути видалення
    app.MapControllerRoute(
        name: "deleteProduct",
        pattern: "product/delete/{id}",
        defaults: new { controller = "Products", action = "DeleteProduct", id = 0 });

    app.MapControllerRoute(
        name: "deleteReview",
        pattern: "review/delete/{id}",
        defaults: new { controller = "Reviews", action = "DeleteReview", id = 0 });

    app.MapControllerRoute(
        name: "deleteComment",
        pattern: "comment/delete/{id}",
        defaults: new { controller = "Comments", action = "DeleteComment", id = 0 });

    app.MapControllerRoute(
        name: "deleteCategory",
        pattern: "category/delete/{id}",
        defaults: new { controller = "Categories", action = "DeleteCategory", id = 0 });
    */
}


await app.RunAsync();
