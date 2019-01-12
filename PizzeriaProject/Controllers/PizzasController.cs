using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzeriaProject.Models;
using PizzeriaProject.ViewModels;
using System.Data.Entity.Infrastructure;

namespace PizzeriaProject.Controllers
{
    public class PizzasController : Controller
    {
        private PizzaDbContext db = new PizzaDbContext();

        // GET: Pizzas
        public ActionResult Index(string searchedPizzaName)
        {
            List<Pizza> pizzas = db.Pizzas.ToList();

            if (!String.IsNullOrEmpty(searchedPizzaName))
            {
                pizzas = pizzas.Where(s => s.Name.ToLower().Contains(searchedPizzaName.ToLower())).ToList();
            }

            return View(pizzas);
        }

        // GET: Pizzas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // GET: Pizzas/Create
        public ActionResult Create()
        {            
            ViewBag.Ingredients = db.Ingredients.ToList();

            return View();
        }

        // POST: Pizzas/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,SmallPrice,MediumPrice,LargePrice,DoughType")] Pizza pizza, string[] selectedIngredients)
        {
            if (ModelState.IsValid)
            {
                if (selectedIngredients == null) selectedIngredients = new string[0];

                List<Ingredient> selectedIngredientList = db.Ingredients.Where(x => selectedIngredients.Contains(x.Id.ToString())).ToList();
                pizza.Ingredients = selectedIngredientList;
                db.Pizzas.Add(pizza);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ingredients = db.Ingredients.ToList();
            return View(pizza);
        }

        // GET: Pizzas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas
            .Include(i => i.Ingredients)
            .Where(i => i.Id == id)
            .Single();

            SetPizzaIngredientsInViewBag(pizza);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedIngredients)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pizza pizzaToUpdate = db.Pizzas
               .Include(i => i.Ingredients)
               .Where(i => i.Id == id)
               .Single();

            if (TryUpdateModel(pizzaToUpdate, "",
               new string[] { "Id", "Name", "SmallPrice", "MediumPrice", "LargePrice", "DoughType" }))
            {
                try
                {
                    UpdatePizzaIngredients(selectedIngredients, pizzaToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            SetPizzaIngredientsInViewBag(pizzaToUpdate);
            return View(pizzaToUpdate);
        }

        private void UpdatePizzaIngredients(string[] selectedIngredients, Pizza pizzaToUpdate)
        {
            if (selectedIngredients == null)
            {
                pizzaToUpdate.Ingredients = new List<Ingredient>();
                return;
            }

            var selectedIngredientsHS = new HashSet<string>(selectedIngredients);
            var pizzaIngredients = new HashSet<int> 
                (pizzaToUpdate.Ingredients.Select(c => c.Id));
            foreach (var ingredient in db.Ingredients)
            {
                if (selectedIngredientsHS.Contains(ingredient.Id.ToString()))
                {
                    if (!pizzaIngredients.Contains(ingredient.Id))
                    {
                        pizzaToUpdate.Ingredients.Add(ingredient);
                    }
                }
                else
                {
                    if (pizzaIngredients.Contains(ingredient.Id))
                    {
                        pizzaToUpdate.Ingredients.Remove(ingredient);
                    }
                }
            }
        }

        // GET: Pizzas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pizza pizza = db.Pizzas.Find(id);
            db.Pizzas.Remove(pizza);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void SetPizzaIngredientsInViewBag(Pizza pizza)
        {
            var allIngredients = db.Ingredients;
            var pizzaIngredients = new HashSet<int>(pizza.Ingredients.Select(c => c.Id));
            var viewModel = new List<AssignedIngredientsData>();
            foreach (var ingredient in allIngredients)
            {
                viewModel.Add(new AssignedIngredientsData
                {
                    IngredientId = ingredient.Id,
                    Name = ingredient.Name,
                    IsAssigned = pizzaIngredients.Contains(ingredient.Id)
                });
            }
            ViewBag.Ingredients = viewModel;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
