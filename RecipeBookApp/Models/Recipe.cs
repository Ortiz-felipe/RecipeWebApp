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

        [Required] //Data Annotation define en la BD que el elemento no admite nulls
        [StringLength(50)] //Define la longitud del string a almacenar, en este caso 50 caracteres
        public string Name { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Ingredients { get; set; }

        public int? TotalViews { get; set; }
    }
}