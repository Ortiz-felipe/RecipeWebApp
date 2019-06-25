using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipeBookApp.Models;

namespace RecipeBookApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            var recipes = context.Recipes.ToList();

            if (!recipes.Any())
            {
                recipes = new List<Recipe>();
            }
            else
            {
                recipes.GetRange(0, 3);
            }

            return View(recipes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}