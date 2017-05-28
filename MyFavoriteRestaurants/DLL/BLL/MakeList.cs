using System.Collections.Generic;
using DLL.BE;
using DLL.Interface;

namespace DLL.BLL
{
    public class MakeList
    {
        private IRespository<Food> _foodRespository = new DLLFacade().GetFoodRepository();
        private IRespository<Restaurant> _restaurantRepository = new DLLFacade().GetRestaurantRepository();

        //fills the database if it empty. 
        public void FillDatabase()
        {
           if (_restaurantRepository.Read().Count == 0)
            {
                var res = new Restaurant
                {
                    Name = "Bones",
                    Address = "BonesAddres",
                    Describing = "kendt for et spareRibs",
                    Webside = "bones.dk",
                    Foods = new List<Food>()

                };

                
                var resWithId = _restaurantRepository.Create(res);
                

                var food = new Food
                {
                    Describing = "smagte godt",
                    Name = "SpareRibs",
                    Price = 120,
                    Rating = 3,
                    RestaurantId = resWithId.Id

                };

                _foodRespository.Create(food);
                res.Foods.Add(food);
                _restaurantRepository.Update(res);
            }
        }

        //finds restaurant by id.
        public Restaurant FindRestaurant(int restaurantId)
        {
            Restaurant foundRestuarant = null;
            foreach (var restaurant in _restaurantRepository.Read())
            {
                if (restaurant.Id == restaurantId)
                {
                    foundRestuarant = restaurant;
                }
            }
            return foundRestuarant;
        }
    }

}
