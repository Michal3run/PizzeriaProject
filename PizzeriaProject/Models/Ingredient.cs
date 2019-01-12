using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaProject.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}