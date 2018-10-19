using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> _restaurant;

        public InMemoryRestaurantData()
        {
            _restaurant = new List<Restaurant>
             {
                 new Restaurant(){ Id = 1, Name = "Pizzeria" },
                 new Restaurant(){ Id = 2, Name = "Resturant" },
                 new Restaurant(){ Id = 3, Name = "Bar" },
             };
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurant.Id = _restaurant.Max(r => r.Id) + 1;
            _restaurant.Add(restaurant);
            return restaurant;
        }

        public Restaurant Get(int id)
        {
            return _restaurant.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurant.OrderBy(r => r.Name);
        }
    }
}
