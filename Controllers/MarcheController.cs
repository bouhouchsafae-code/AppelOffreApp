using AppelOffreApp.Models;
using AppelOffreApp.Services;
using Microsoft.AspNetCore.Mvc;

public class MarcheController : Controller
{
    private readonly DatabaseService _db;

    public MarcheController(DatabaseService db) => _db = db;

    public IActionResult Create(int appelOffreId, int entrepriseId)
    {
        ViewBag.AppelOffreId = appelOffreId;
        ViewBag.EntrepriseId = entrepriseId;
        return View();
    }

    [HttpPost]
    public IActionResult Create(Marche marche)
    {
        _db.Marches.Insert(marche);
        var appel = _db.AppelsOffres.FindById(marche.AppelOffreId);
        if (appel != null)
        {
            appel.Statut = "Attribué";
            _db.AppelsOffres.Update(appel);
        }
        return RedirectToAction("Liste");
    }

    public IActionResult Liste()
    {
        var marches = _db.Marches.FindAll().ToList();
        return View(marches);
    }
}