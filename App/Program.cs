using Data;
using Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Service.Customer;
using Service.Load;
using TimeUtils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IClock>(SystemClock.Instance);
builder.Services.AddSingleton(DateTimeZoneProviders.Tzdb);
builder.Services.AddSingleton<LocalTimeWindow.Factory>();
builder.Services.AddScoped<AuditInterceptor>();
builder.Services.AddDbContext<FriendlyDbContext>((sp, options) =>
{
    options.UseSqlite("Data Source=ormtalk.db", x => x.UseNodaTime());
});
builder.Services.AddScoped<ILoadService, LoadService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<FriendlyDbContext>();
    await db.Database.MigrateAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute("default", "{controller=Load}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();
