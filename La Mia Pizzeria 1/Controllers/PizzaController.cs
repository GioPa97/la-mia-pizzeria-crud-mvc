using La_Mia_Pizzeria_1.Database;
using La_Mia_Pizzeria_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




namespace La_Mia_Pizzeria_1.Controllers
{
    public class PizzaController : Controller
    {

        public IActionResult Index()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> listaDeiPost = db.Pizze.ToList<Pizza>();
                return View("Index", listaDeiPost);
            }

        }

        public IActionResult Prodotti()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> listaDeiPost = db.Pizze.ToList<Pizza>();
                return View("Prodotti", listaDeiPost);
            }

        }

        public IActionResult Details(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                // LINQ: syntax methos
                Pizza postTrovato = db.Pizze
                    .Where(SingoloPostNelDb => SingoloPostNelDb.Id == id)
                    .Include(pizze => pizze.Category)
                    .FirstOrDefault();

                if (postTrovato != null)
                {
                    return View(postTrovato);
                }

                return NotFound("Il post con l'id cercato non esiste!");

            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Category> categoriesFromDb = db.Categories.ToList<Category>();

                PostCategoriesView modelForView = new PostCategoriesView();
                modelForView.Pizze = new Pizza();

                modelForView.Categories = categoriesFromDb;

                return View("Create", modelForView);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostCategoriesView formData)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    List<Category> categories = db.Categories.ToList<Category>();

                    formData.Categories = categories;
                }


                return View("Create", formData);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Pizze.Add(formData.Pizze);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza postToUpdate = db.Pizze.Where(articolo => articolo.Id == id).FirstOrDefault();

                if (postToUpdate == null)
                {
                    return NotFound("Il post non è stato trovato");
                }

                List<Category> categories = db.Categories.ToList<Category>();

                PostCategoriesView modelForView = new PostCategoriesView();
                modelForView.Pizze = postToUpdate;
                modelForView.Categories = categories;

                return View("Update", modelForView);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PostCategoriesView formData)
        {
            if (!ModelState.IsValid)
            {

                using (PizzeriaContext db = new PizzeriaContext())
                {
                    List<Category> categories = db.Categories.ToList<Category>();

                    formData.Categories = categories;
                }

                return View("Update", formData);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza postToUpdate = db.Pizze.Where(articolo => articolo.Id == id).FirstOrDefault();

                if (postToUpdate != null)
                {

                    postToUpdate.Title = formData.Pizze.Title;
                    postToUpdate.Description = formData.Pizze.Description;
                    postToUpdate.Image = formData.Pizze.Image;
                    postToUpdate.Prezzo = formData.Pizze.Prezzo;
                    postToUpdate.CategoryId = formData.Pizze.CategoryId;

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
                Pizza postToDelete = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (postToDelete != null)
                {
                    db.Pizze.Remove(postToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Il post da eliminare non è stato trovato!");
                }
            }
        }

        [HttpDelete]
        public IActionResult ProvaDelete()
        {
            return View("Create");
        }

    }

}

