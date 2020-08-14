using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers
{
    public class HomeController : Controller
    {
        private ChefsNDishesContext db;

        public HomeController(ChefsNDishesContext context)
        {
            db = context;
        }
        
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> chefs = db.Chefs
                .Include(chef => chef.CreatedDishes)
                .ToList();
            return View("Index", chefs);
        }

        [HttpGet("new")]
        public IActionResult NewChef()
        {
            return View("NewChef");
        }

        [HttpPost("create/chef")]
        public IActionResult CreateChef(Chef newChef)
        {
            if (ModelState.IsValid)
            {
                if (newChef.DOB > DateTime.Today)
                {
                    ModelState.AddModelError("DOB", "Date cannot be a futur date.");
                    return View("NewChef");
                }

                db.Add(newChef);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewChef");
        }

        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            List<Dish> dishes = db.Dishes
                .Include(dish => dish.Creator)
                .ToList();
            return View("Dishes", dishes);
        }

        [HttpGet("dishes/new")]
        public IActionResult NewDish()
        {
            List<Chef> chefs = db.Chefs.ToList();
            ViewBag.Chefs = chefs;
            return View("NewDish");
        }

        [HttpPost("create/dish")]
        public IActionResult CreateDish(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                db.Add(newDish);
                db.SaveChanges();
                return RedirectToAction("Dishes");
            }
            
            List<Chef> chefs = db.Chefs.ToList();
            ViewBag.Chefs = chefs;
            return View("NewDish");
        }
    }
}
