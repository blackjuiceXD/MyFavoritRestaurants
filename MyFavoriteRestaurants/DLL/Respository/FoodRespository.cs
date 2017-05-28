using System.Collections.ObjectModel;
using System.Linq;
using DLL.BE;
using DLL.Interface;
using SQLite.Net;
using Xamarin.Forms;

namespace DLL.Respository
{
    class FoodRespository : IRespository<Food>
    {
        private SQLiteConnection _connection;

        public FoodRespository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<Food>();

        }
        //creates food
        public Food Create(Food t)
        {
            _connection.Insert(t);
            return (t);
        }

        //get food by Id
        public Food Read(int id)
        {
            return _connection.Table<Food>().FirstOrDefault(t => t.Id == id);
        }

        //gets a list of food
        public ObservableCollection<Food> Read()
        {
            ObservableCollection<Food> foods =
                new ObservableCollection<Food>(from t in _connection.Table<Food>() select t);
            return foods;
        }

        //update food
        public Food Update(Food t)
        {
            if (t.Id != 0)
            {
                _connection.Update(t);
                return t;
            }
            return null;
        }

        //delete food
        public bool Delete(int id)
        {
            _connection.Delete<Food>(id);
            return true;
        }

        
    }
}

