using La_Mia_Pizzeria_1.Models;
namespace NetCore_01.Models

{
    public class Category
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public List<Pizza> Pizzas { get; set; }

        public Category()
        {

        }
    }
}
