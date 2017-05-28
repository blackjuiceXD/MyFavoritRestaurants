using System.Collections.ObjectModel;
using System.Linq;
using DLL.BE;
using DLL.Interface;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;

namespace DLL.Respository
{
    class RestaurantRespository : IRespository<Restaurant>
    {
        private SQLiteConnection _connection;

        public RestaurantRespository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<Restaurant>();

        }

        //creates a Restaurant
        public Restaurant Create(Restaurant t)
        {
            _connection.InsertWithChildren(t, true);
            return t;
        }

        //gets restaurant by id
        public Restaurant Read(int id)
        {
            return _connection.GetAllWithChildren<Restaurant>().FirstOrDefault(t => t.Id == id);
        }

        //gets a list of restaurants
        public ObservableCollection<Restaurant> Read()
        {
            ObservableCollection<Restaurant> restaurants =
                new ObservableCollection<Restaurant>(from t in _connection.GetAllWithChildren<Restaurant>() select t);
            return restaurants;

        }

        //updates restaurant
        public Restaurant Update(Restaurant t)
        {
            if (t.Id != 0)
            {
                _connection.UpdateWithChildren(t);
                return t;
            }
            return null;
        }

        //delete restaurant
        public bool Delete(int id)
        {
            _connection.Delete<Restaurant>(id);
            return true;
        }
    }
}
