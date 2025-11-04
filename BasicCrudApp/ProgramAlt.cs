//using Microsoft.EntityFrameworkCore;
//using BasicCrudApp.Data;
//using BasicCrudApp.Models;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddControllers();

//var app = builder.Build();

//app.MapGet("/api/products", async (AppDbContext db) => await db.Products.ToListAsync());

//app.MapPost("/api/products", async (AppDbContext db, Product product) =>
//{
//    db.Products.Add(product);
//    await db.SaveChangesAsync();
//    return Results.Created($"/api/products/{product.Id}", product);
//});

//app.MapPut("/api/products/{id}", async (AppDbContext db, int id, Product input) =>
//{
//    var product = await db.Products.FindAsync(id);
//    if (product is null) return Results.NotFound();
//    product.Name = input.Name;
//    product.Description = input.Description;
//    product.Price = input.Price;
//    await db.SaveChangesAsync();
//    return Results.NoContent();
//});

//app.MapDelete("/api/products/{id}", async (AppDbContext db, int id) =>
//{
//    var product = await db.Products.FindAsync(id);
//    if (product is null) return Results.NotFound();
//    db.Products.Remove(product);
//    await db.SaveChangesAsync();
//    return Results.NoContent();
//});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Products}/{action=Index}/{id?}"
//);

//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();