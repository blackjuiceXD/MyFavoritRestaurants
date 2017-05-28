using DLL.BE;
using DLL.Interface;
using DLL.Respository;

namespace DLL
{
    public class DLLFacade
    {
        public IRespository<Restaurant> GetRestaurantRepository()
        {
            return new RestaurantRespository();
        }

        public IRespository<Food> GetFoodRepository()
        {
            return new FoodRespository();
        }
    }
}
