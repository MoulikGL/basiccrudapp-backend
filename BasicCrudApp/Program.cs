using Microsoft.EntityFrameworkCore;
using BasicCrudApp.Data;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddCors(options => options.AddPolicy("AllowFrontend", policy => policy.WithOrigins("http://localhost:5173", "https://basiccrudapp.vercel.app").AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

app.UseCors("AllowFrontend");
//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();