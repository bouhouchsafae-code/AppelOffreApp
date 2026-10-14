using AppelOffreApp.Models;
using AppelOffreApp.Services;
using Microsoft.AspNetCore.Mvc;

public class AuthController : Controller
{
    private readonly DatabaseService _db;

    public AuthController(DatabaseService db) => _db = db;

    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(Utilisateur utilisateur)
    {
        if (_db.Utilisateurs.Exists(u => u.Email == utilisateur.Email))
        {
            ModelState.AddModelError("Email", "Email déjà utilisé.");
            return View(utilisateur);
        }

        if (string.IsNullOrEmpty(utilisateur.Role))
            utilisateur.Role = "Entreprise";

        _db.Utilisateurs.Insert(utilisateur);
        return RedirectToAction("Login");
    }

    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(string email, string motDePasse)
    {
        var user = _db.Utilisateurs.FindOne(u => u.Email == email && u.MotDePasse == motDePasse);

        if (user == null)
        {
            ModelState.AddModelError("", "Email ou mot de passe invalide.");
            return View();
        }

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserRole", user.Role);
        HttpContext.Session.SetString("UserName", user.Nom);

        return user.Role switch
        {
            "Admin" => RedirectToAction("Index", "Admin"),
            "ChefService" => RedirectToAction("Index", "ChefService"),
            "Entreprise" => RedirectToAction("Index", "Entreprise"),
            _ => RedirectToAction("AccessDenied")
        };
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public IActionResult AccessDenied() => View();
}
