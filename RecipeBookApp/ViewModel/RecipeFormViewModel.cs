using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipeBookApp.Models;

namespace RecipeBookApp.ViewModel
{
    public class RecipeFormViewModel
    {
        public Recipe Recipe { get; set; }

        public string Title
        {
            get
            {
                if (Recipe != null && Recipe.Id != 0)
                {
                    return "Edit Recipe";
                }

                return "New Recipe";
            }

        }
    }
}