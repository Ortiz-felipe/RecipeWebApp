﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipeBookApp.Models;

namespace RecipeBookApp.Controllers
{
    public class RecipiesController : Controller
    {
        private ApplicationDbContext _context;

        public RecipiesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Recipies
        public ActionResult Index()
        {
            return View();
        }
    }
}