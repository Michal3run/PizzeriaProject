using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;

namespace PizzeriaProject.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(0.0, 99999)]
        [DisplayName("Small size price")]
        public decimal SmallPrice { get; set; }

        [Required]
        [Range(0.0, 99999)]
        [DisplayName("Medium size price")]
        public decimal MediumPrice { get; set; }

        [Required]
        [Range(0.0, 99999)]
        [DisplayName("Large size price")]
        public decimal LargePrice { get; set; }

        [Required]
        [DisplayName("Dough type")]
        public EPizzaDough DoughType { get; set; }


        [DisplayName("Ingredients")]
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}