using Collecting.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Collecting.Data;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Collecting.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            db = context;
            
        }

        public async Task<IActionResult> Index(string? id)
        {
            if (id != null)
            {
                return View("Index", await db.Collections.Where(j => j.UserId == id).ToListAsync());
            }
            return View(await db.Collections.ToListAsync());
        }

        [Authorize(Roles = "active")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "active")]
        [HttpPost]
        public async Task<IActionResult> Create(Collection col)
        {
            col.UserId = User.Identity?.Name!;
            db.Collections.Add(col);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "active")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string returnUrl)
        {
            var culture = CultureInfo.CurrentCulture.Name == "ru" ? "en" : "ru";
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string SearchTerm)
        {
            return View("Items", await db.Items.Where( j => j.Name.Contains(SearchTerm)).ToListAsync());
        }

        public async Task<IActionResult> Items(int id)
        {
            ViewBag.Id = id;
            return View(await db.Items.Where(j => j.CollectionId == id).ToListAsync());
        }

        public IActionResult Add(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Items item, int temp)
        {
            //using (var transaction = db.Database.BeginTransaction())
            //{
            //    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Collections OFF;");
            //    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Items ON;");
            //    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Items OFF;");
            //    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Collections ON;");
            //    db.SaveChanges();
            //    transaction.Commit();
            //}
            item.CollectionId = temp;
            db.Items.Add(item);
            await db.SaveChangesAsync();
            return RedirectToAction("Items", new { id = temp});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}