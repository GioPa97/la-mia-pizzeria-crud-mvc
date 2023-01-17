using La_Mia_Pizzeria_1.Database;
using La_Mia_Pizzeria_1.Models;
using Microsoft.AspNetCore.Mvc;




namespace La_Mia_Pizzeria_1.Controllers
{
    public class PizzaController : Controller
    {


        public IActionResult Index()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> listaDellePizza = db.Pizzas.OrderBy(title => title.Title).ToList<Pizza>();

                ; return View("Index", listaDellePizza);
            }

        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            bool FunzioneDiRicercaPostById(Pizza pizza)
            {
                return pizza.Id == id;
            }


            using (PizzeriaContext db = new PizzeriaContext())
            {
                // LINQ: syntax methos
                Pizza pizzaTrovato = db.Pizzas
                    .Where(SingolaPizzaNelDb => SingolaPizzaNelDb.Id == id)
                    .FirstOrDefault();

                // LINQ: query syntax
                Pizza pizzaTrovata =
                    (from Pizza in db.Pizzas
                     where Pizza.Id == id
                     select Pizza).FirstOrDefault<Pizza>();



                if (pizzaTrovato != null)
                {
                    return View(pizzaTrovato);
                }

                return NotFound("la pizza con l'id cercato non esiste!");

            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", formData);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Pizzas.Add(formData);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza postToUpdate = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (postToUpdate == null)
                {
                    return NotFound("Il post non è stato trovato");
                }

                return View("Update", postToUpdate);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Pizza formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", formData);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza postToUpdate = db.Pizzas.Where(articolo => articolo.Id == formData.Id).FirstOrDefault();

                if (postToUpdate != null)
                {
                    postToUpdate.Title = formData.Title;
                    postToUpdate.Description = formData.Description;
                    postToUpdate.Image = formData.Image;
                    postToUpdate.Prezzo = formData.Prezzo;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Il post che volevi modificare non è stato trovato!");
                }
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {

                Pizza pizzaToDelete = db.Pizzas.Where(Pizza => Pizza.Id == id).FirstOrDefault();
                if (pizzaToDelete != null)
                {
                    db.Pizzas.Remove(pizzaToDelete);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }









    }
}
