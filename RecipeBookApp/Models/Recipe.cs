using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace RecipeBookApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a Recipe Name")] //Data Annotation define en la BD que el elemento no admite nulls
        [StringLength(50)] //Define la longitud del string a almacenar, en este caso 50 caracteres
        public string Name { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a description for the recipe")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please provide the ingredients for the recipe")]
        public string Ingredients { get; set; }

        [Display(Name = "Total Views")]//Data Annotation, con este valor es que se mostrara en la vista
        public int? TotalViews { get; set; }

    }
}