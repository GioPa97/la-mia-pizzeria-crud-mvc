

namespace La_Mia_Pizzeria_1.Models

{
    public class Category
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<Pizza> Pizze { get; set; }

        public Category()
        {

        }
    }
}
