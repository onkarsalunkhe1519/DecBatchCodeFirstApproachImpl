using DecBatchCodeFirstApproachImpl.Data;
using DecBatchCodeFirstApproachImpl.Filter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<CustomActionFilter>();
}
);

builder.Services.AddDbContext<ApplicationDbContext>
    (
        options=>options.UseSqlServer
        (
            builder.Configuration.GetConnectionString("dbconn")    
        )
    );

builder.Services.AddSession
    (
        options=>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(5);
            options.Cookie.IsEssential = true;
            options.Cookie.HttpOnly = true;
        }
    );

builder.Services.AddHttpContextAccessor();

var app = builder.Build();


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
app.UseSession();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=SignIn}/{id?}");

app.Run();
