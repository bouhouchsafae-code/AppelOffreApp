using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("UserRole") != "Admin")
            return RedirectToAction("AccessDenied", "Auth");

        return View(); // utilisera Views/Admin/Index.cshtml avec LayoutAdmin
    }
}
