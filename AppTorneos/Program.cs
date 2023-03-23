using AppTorneos.Data;
using AppTorneos.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(180);
});
string connectionString =
    builder.Configuration.GetConnectionString("TorneoBBDD");
builder.Services.AddTransient<RepositoryUsuarios>();
builder.Services.AddTransient<RepositoryEquipos>();
builder.Services.AddTransient<RepositoryLigas>();
builder.Services.AddDbContext<BSTournamentContext>
    (options => options.UseSqlServer(connectionString));


builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LoginUsuario}/{action=InicioPagina}");

app.Run();

