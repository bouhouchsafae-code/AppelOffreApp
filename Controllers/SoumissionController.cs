using AppelOffreApp.Models;
using AppelOffreApp.Services;
using Microsoft.AspNetCore.Mvc;

public class SoumissionController : Controller
{
    private readonly DatabaseService _db;

    public SoumissionController(DatabaseService db) => _db = db;

    public IActionResult Submit(int id)
    {
        ViewBag.AppelOffreId = id;
        return View();
    }

    [HttpPost]
    public IActionResult Submit(Soumission s)
    {
        s.DateSoumission = DateTime.Now;
        _db.OffresSoumises.Insert(s);
        return RedirectToAction("MesSoumissions");
    }

    public IActionResult MesSoumissions()
    {
        var list = _db.OffresSoumises.FindAll().ToList();
        return View(list);
    }
}
