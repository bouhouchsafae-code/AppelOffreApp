using AppelOffreApp.Services;
using LiteDB;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Services
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<LiteDatabase>(new LiteDatabase("Filename=appeloffre.db;Connection=shared;password=longChee+ah47"));
builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddSingleton<ThemeInitializerService>(); // 👈 Ajouter le service d'initialisation

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 🌱 Initialiser les thèmes si nécessaire
using (var scope = app.Services.CreateScope())
{
    var themeInitializer = scope.ServiceProvider.GetRequiredService<ThemeInitializerService>();
    themeInitializer.InitialiserThemesSiVide();
}

// ⚙️ Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// 🚪 Routes
app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
