namespace PizzeriaProject.Models
{
    public class PizzaPrice
    {
        public int Small { get; set; }
        public int Medium { get; set; }
        public int Large { get; set; }

        public PizzaPrice()
        {

        }

        public PizzaPrice(int small, int medium, int large)
        {
            this.Small = small;
            this.Medium = medium;
            this.Large = large;
        }

        public override string ToString()
        {
            return $"{Small}, {Medium}, {Large}";
        }
    }
}