using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Models;

namespace Restaurants.Controllers
{
    public class RestaurantController : Controller
    {
        [HttpGet("/restaurants")]
        public ActionResult Index()
        {
            Dictionary<string, object> restaurantCuisine = new Dictionary<string, object>();
            List<RestaurantClass> restaurantList = RestaurantClass.GetAll();
            List<CuisineClass> cuisineList = CuisineClass.GetAll();
            restaurantCuisine.Add("restaurant", restaurantList);
            restaurantCuisine.Add("cuisine", cuisineList);
            return View(restaurantCuisine);
        }

        [HttpGet("/restaurants/newcuisine")]
        public ActionResult NewCuisine()
        {
            return View();
        }

        [HttpPost("/restaurants/newcuisine")]
        public ActionResult Create(string cuisineName)
        {
            CuisineClass current = new CuisineClass(cuisineName);
            current.Save();
            List<CuisineClass> cuisineList = CuisineClass.GetAll();
            return View("NewRestaurant", cuisineList);
        }

        [HttpPost("/restaurants/newrestaurant")]
        public ActionResult Create(string restaurantName, string restaurantLocation, string restaurantCuisine)
        {
            int restaurantCuisineNew = int.Parse(restaurantCuisine);
            RestaurantClass restaurant = new RestaurantClass(restaurantName, restaurantLocation, restaurantCuisineNew);
            restaurant.Save();

            Dictionary<string, object> model = new Dictionary<string, object>();
            List<RestaurantClass> restaurantList = RestaurantClass.GetAll();
            List<CuisineClass> cuisineList = CuisineClass.GetAll();
            model.Add("restaurant", restaurantList);
            model.Add("cuisine", cuisineList);
            return View("Index", model);
        }

        [HttpGet("/restaurants/newrestaurant")]
        public ActionResult Show()
        {
            List<CuisineClass> cuisineList = CuisineClass.GetAll();
            return View("NewRestaurant", cuisineList);
        }

        [HttpGet("/restaurants/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string, object> dick = new Dictionary<string, object>();
            List<RestaurantClass> restaurantList = RestaurantClass.FindById(id);
            List<CuisineClass> cuisineList = CuisineClass.FindById(restaurantList[0].GetCuisineId());
            dick.Add("restaurant", restaurantList);
            dick.Add("cuisine", cuisineList);
            return View(dick);
        }

        [HttpGet("/restaurants/{id}/delete")]
        public ActionResult Destroy(int id)
        {
            RestaurantClass.Delete(id);
            return View("Delete");
        }

        [HttpGet("/cuisine/{id}")]
        public ActionResult CuisineDetails(int id)
        {
            Dictionary<string, object> megAdick = new Dictionary<string, object>();
            List<CuisineClass> cuisineList = CuisineClass.FindById(id);
            List<RestaurantClass> restaurantList = RestaurantClass.GetAllRestaurants(id);
            megAdick.Add("restaurant" , restaurantList);
            megAdick.Add("cuisine" , cuisineList);
            return View("ShowCuisine" , megAdick);
        }

        [HttpGet("/restaurants/cuisine/{id}/delete")]
        public ActionResult CuisineDelete(int id)
        {
            RestaurantClass.DeleteRestaurantByCuisineId(id);
            CuisineClass.DeleteCuisine(id);
            return View("Delete");
        }
    }
}
