using AppelOffreApp.Models;
using AppelOffreApp.Services;
using Microsoft.AspNetCore.Mvc;

public class EntrepriseController : Controller
{
    private readonly DatabaseService _db;

    public EntrepriseController(DatabaseService db) => _db = db;

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("UserRole") != "Entreprise")
            return RedirectToAction("AccessDenied", "Auth");

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        var user = _db.Utilisateurs.FindById(userId.Value);

        if (string.IsNullOrEmpty(user.Theme))
            return RedirectToAction("ChoisirTheme");

        ViewBag.Theme = user.Theme;
        return View(user);
    }

    [HttpGet]
    public IActionResult ChoisirTheme()
    {
        ViewBag.Themes = _db.Themes.FindAll().ToList();
        return View();
    }

    [HttpPost]
    public IActionResult ChoisirTheme(string theme)
    {
        if (string.IsNullOrWhiteSpace(theme))
        {
            ModelState.AddModelError("", "Veuillez sélectionner un thème.");
            return RedirectToAction("ChoisirTheme");
        }

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        var user = _db.Utilisateurs.FindById(userId.Value);
        user.Theme = theme;
        _db.Utilisateurs.Update(user);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModifierTheme()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        ViewBag.Themes = _db.Themes.FindAll().ToList();
        ViewBag.CurrentTheme = _db.Utilisateurs.FindById(userId.Value).Theme;

        return View();
    }

    [HttpPost]
    public IActionResult ModifierTheme(string theme)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        var user = _db.Utilisateurs.FindById(userId.Value);
        user.Theme = theme;
        _db.Utilisateurs.Update(user);

        return RedirectToAction("Index");
    }
}