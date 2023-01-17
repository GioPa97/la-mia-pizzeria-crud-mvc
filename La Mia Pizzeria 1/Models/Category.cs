using La_Mia_Pizzeria_1.Models;
namespace NetCore_01.Models

{
    public class Category
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public List<Pizza> Pizze { get; set; }

        public Category()
        {

        }
    }
}
