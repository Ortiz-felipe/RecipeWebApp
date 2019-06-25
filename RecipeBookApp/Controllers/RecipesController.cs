using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RecipeBookApp.Models;
using RecipeBookApp.ViewModel;

namespace RecipeBookApp.Controllers
{    
    public class RecipesController : Controller
    {
        //Requerido para tener el contexto de la app
        //De esta forma se podran visualizar los contenidos de las recetas en la base de datos
        private ApplicationDbContext _context;

        public RecipesController()
        {
            _context = new ApplicationDbContext();
        }

        //Se encargara de deshacerse del contexto de la app
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Recipies
        public ActionResult Index()
        {
            //Inicializamos una variable que contendra las recetas de la base de datos
            //Esto se logra, accediendo al atributo _context, luego a su contexto "Recipies" el cual responde a un DbSet<Recipe>
            //Al hacer eso, no generara una query a la BD para obtener el contenido.
            //A continuacion, existen dos formas, en diferido y delegando la responsabilidad de iterar en la vista
            //O, al final de la instruccion, llamar al metodo


            //Esto son lineas de prueba, lo que se acaba de realizar en la siguiente linea, es llamar a un metodo GetRecipes(), que
            //Devuelve un IEnumerable de recetas. simplemente con los fines de probar la funcionalidad de Index y Edit
            //IEnumerable es una Interfaz de List(), que devuelve una lista sin los metodos nativos de una Lista (Tales como agregar, eliminar, etc)
            var recipes = _context.Recipes.ToList();

            return View(recipes);
        }

        public ActionResult GetRecipesByUserId()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.Identity.GetUserId();
            IEnumerable<Recipe> recipesByUserId = _context
            .Recipes
            .Where(r => r.UserId.Contains(userId))
            .ToList();

            return View(recipesByUserId);

        }

        //Metodo encargado de crear el modelo del formulario de la receta
        //GET
        [HttpGet]        
        public ActionResult New()
        {
            
            var viewModel = new RecipeFormViewModel
            {
                Recipe = new Recipe()
            };

            return View("RecipeForm", viewModel);
        }

        //Metodo encargado de la validacion del modelo del formulario de la receta
        [HttpPost]        
        [ValidateAntiForgeryToken]
        public ActionResult Save(Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new RecipeFormViewModel();

                return View("RecipeForm", viewModel);
            }

            if (recipe.Id == 0)
            {
                recipe.UserId = HttpContext.User.Identity.GetUserId();
                recipe.UserName = HttpContext.User.Identity.GetUserName();
                recipe.TotalViews = 0;
                _context.Recipes.Add(recipe);
            }
            else
            {
                var recipeInDb = _context.Recipes.Single(r => r.Id == recipe.Id);

                recipeInDb.Name = recipe.Name;
                recipeInDb.Description = recipe.Description;
                recipeInDb.Ingredients = recipe.Ingredients;

            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Recipes");

        }

                        
        public ActionResult Edit(int id)
        {
            //El resultado de esta asignacion a var recipe, devuelve el primer elemento de la lista que coincida con el valor pasado por parametro
            //Usaremos LINQ con una expresion LAMBDA para obtener dicho elemento
            //Esto ( r => r.id == id), que se lee como "r tal que r.id == id", buscara el primer elemento dentro de la lista, que coincida con id
            //Estas mismas expresiones las usaremos para hacer peticiones a la base de datos
            var recipe = _context.Recipes.SingleOrDefault(r => r.Id == id);

            if (recipe == null)
            {
                return HttpNotFound();
            }            

            var viewModel = new RecipeFormViewModel
            {
                Recipe = recipe
            };

            return View("RecipeForm", viewModel);
            
        }

        
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var recipe = _context.Recipes.SingleOrDefault(r => r.Id == id);           

            if (recipe == null)
            {
                return HttpNotFound();
            }

            recipe.TotalViews++;

            _context.SaveChanges();

            if (recipe.UserId != User.Identity.GetUserId())
            {
                return View("SimpleDetails", recipe);
            }            

            return View(recipe);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var recipes = _context.Recipes.SingleOrDefault(r => r.Id == id);

            if (recipes == null)
            {
                return HttpNotFound();
            }


            return View(recipes);
        }


        //POST recipes/recipe/1
        public ActionResult ConfirmDelete(int id)
        {
            var recipe = _context.Recipes.SingleOrDefault(r => r.Id == id);           

            _context.Recipes.Remove(recipe);
            _context.SaveChanges();

            return RedirectToAction("GetRecipesByUserId", "Recipes");
        }       
    }
}