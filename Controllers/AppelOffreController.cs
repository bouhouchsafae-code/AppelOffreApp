using AppelOffreApp.Models;
using AppelOffreApp.Services;
using LiteDB;
using Microsoft.AspNetCore.Mvc;

public class AppelOffreController : Controller
{
    private readonly DatabaseService _db;

    public AppelOffreController(DatabaseService db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Create()
    {
        if (HttpContext.Session.GetString("UserRole") != "Admin")
            return RedirectToAction("AccessDenied", "Auth");

        return View();
    }

    [HttpPost]
    public IActionResult Create(AppelOffre appel, IFormFile pdfFile)
    {
        if (HttpContext.Session.GetString("UserRole") != "Admin")
            return RedirectToAction("AccessDenied", "Auth");

        if (pdfFile == null || pdfFile.Length == 0)
        {
            ModelState.AddModelError("", "Le fichier PDF est requis.");
            return View(appel);
        }

        // Générer un ID unique pour le fichier
        var fileId = Guid.NewGuid().ToString();

        // 📦 Stocker le fichier dans LiteDB (FileStorage)
        using (var stream = pdfFile.OpenReadStream())
        {
            _db.Database.FileStorage.Upload(fileId, pdfFile.FileName, stream);
        }

        // Enregistrer les infos dans l'appel d'offre
        appel.FichierPdfId = fileId;
        appel.DatePublication = DateTime.Now;
        appel.Statut = "Ouvert";
        appel.CreeParId = HttpContext.Session.GetInt32("UserId") ?? 0;

        _db.AppelsOffres.Insert(appel);

        return RedirectToAction("List");
    }

    public IActionResult List()
    {
        var appels = _db.AppelsOffres.FindAll().ToList();
        return View(appels);
    }

    public IActionResult Details(int id)
    {
        var appel = _db.AppelsOffres.FindById(id);
        return View(appel);
    }

    // Pour télécharger un fichier PDF depuis LiteDB
    public IActionResult TelechargerPdf(string id)
    {
        var file = _db.Database.FileStorage.FindById(id);
        if (file == null) return NotFound();

        return File(file.OpenRead(), "application/pdf", file.Filename);
    }
}
