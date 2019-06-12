using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipeBookApp.Models;

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
            var recipies = _context.Recipes.ToList();

            return View(recipies);
        }

        public ActionResult Edit(int id)
        {
            throw new NotImplementedException();
        }
    }
}