using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;


namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            
    
            return View();
        }
        [HttpPost]
        [Route("process")]
        public IActionResult Index(Dish newdish)
        
        { 
            dbContext.Dishes.Add(newdish);
            dbContext.SaveChanges();

            
            return Redirect("/");
        }

        [HttpGet]
        [Route("alldishes")]

        public IActionResult AllDishes()
        {

            List<Dish> AllDishes=dbContext.Dishes.ToList();
            return View(AllDishes);
        }

        [HttpGet]
        [Route("dish/{dishId}")]

        public IActionResult SingleDish(int dishId)
        {
            
            Dish DatabaseDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            Console.WriteLine("$$$$$$$$$$$$$");
            Console.WriteLine(DatabaseDish.Name);
            return View(DatabaseDish);
        }

        [HttpGet]
        [Route("editdish/{dishId}")]

        public IActionResult EditDish(int dishId)
        {
            
            Dish DatabaseDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            Console.WriteLine("$$$$$$$$$$$$$");
            Console.WriteLine(DatabaseDish.Name);
            return View(DatabaseDish);
        }
        [HttpPost]
        [Route("processupdate/{dishId}")]
        public IActionResult ProcessUpdate(int dishId, Dish d)
        {
            
            Dish DatabaseDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            DatabaseDish.Name=d.Name;
            DatabaseDish.Chef=d.Chef;
            DatabaseDish.Calories=d.Calories;
            DatabaseDish.Description=d.Description;
            DatabaseDish.Tastiness=d.Tastiness;
            DatabaseDish.UpdatedAt=DateTime.Now;

            dbContext.SaveChanges();
            return Redirect("/alldishes");
        }
        [HttpGet]
        [Route("deletedish/{dishId}")]
        public IActionResult Delete(int dishId)

        {
            Dish DatabaseDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            dbContext.Dishes.Remove(DatabaseDish);
            dbContext.SaveChanges();

            return Redirect("/alldishes");

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
