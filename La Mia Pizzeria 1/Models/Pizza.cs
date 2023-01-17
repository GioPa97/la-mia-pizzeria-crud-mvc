using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace La_Mia_Pizzeria_1.Models
{

    public class Pizza
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        [StringLength(100, ErrorMessage = "Il testo non deve superare i 100 caratteri")]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        [StringLength(500, ErrorMessage = "Il testo non deve superare i 500 caratteri")]

        public string Description { get; set; }

        [Column(TypeName = "varchar(500)")]
        [StringLength(500, ErrorMessage = "Il testo non deve superare i 500 caratteri")]
        [Url]

        public string Image { get; set; }

        public double Prezzo { get; set; }




        public Pizza()
        {

        }

        public Pizza(string title, string description, string image, double prezzo)
        {
            Title = title;
            Description = description;
            Image = image;
            Prezzo = prezzo;
        }


    }
}
