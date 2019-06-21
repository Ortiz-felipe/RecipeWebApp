using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipeBookApp.Models;
using RecipeBookApp.ViewModel;

namespace RecipeBookApp.Controllers
{
    public class RecipiesController : Controller
    {
        //Requerido para tener el contexto de la app
        //De esta forma se podran visualizar los contenidos de las recetas en la base de datos
        private ApplicationDbContext _context;

        public RecipiesController()
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

        public ActionResult New()
        {
            
            var viewModel = new RecipeFormViewModel
            {
                Recipe = new Recipe()
            };

            return View("RecipeForm", viewModel);
        }


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
            return RedirectToAction("Index", "Recipies");

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

        public ActionResult Details(int id)
        {
            var recipe = _context.Recipes.SingleOrDefault(r => r.Id == id);

            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipe);
        }

        private IEnumerable<Recipe> GetRecipes()
        {
            var recipeList = new List<Recipe>
            {
                new Recipe
                {
                    Id = 1,
                    UserId = 1,
                    Name = "First Recipe",
                    Description = "Here it's the recipe description",
                    Ingredients = "This is going to be a list of ingredients to show on the client",
                    TotalViews = 0
                },

                new Recipe
                {
                    Id = 2,
                    UserId = 1,
                    Name = "Second Recipe",
                    Description = "Here it's the recipe description",
                    Ingredients = "This is going to be a list of ingredients to show on the client",
                    TotalViews = 5
                },

                new Recipe
                {
                    Id = 3,
                    UserId = 2,
                    Name = "Third Recipe",
                    Description = "Here it's the recipe description",
                    Ingredients = "This is going to be a list of ingredients to show on the client",
                    TotalViews = 4
                },

                new Recipe
                {
                    Id = 4,
                    UserId = 2,
                    Name = "Fourth Recipe",
                    Description = "Here it's the recipe description",
                    Ingredients = "This is going to be a list of ingredients to show on the client",
                    TotalViews = 100
                }
            };

            return recipeList;
        }

        
    }
}