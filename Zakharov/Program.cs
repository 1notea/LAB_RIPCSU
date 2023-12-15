using Microsoft.EntityFrameworkCore;
using Zakharov;
using Zakharov.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<JournalService>();
builder.Services.AddScoped<JournalRepository>();
builder.Services.AddHostedService<JournalEntryGeneratorService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Journal}/{action=Index}/{id?}");

app.Run();
