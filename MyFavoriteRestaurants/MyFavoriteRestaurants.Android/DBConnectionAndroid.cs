using System.IO;
using System;
using MyFavoriteRestaurants.Droid;
using DLL.Interface;
using SQLite.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(DBConnectionAndroid))]
namespace MyFavoriteRestaurants.Droid
{

    class DBConnectionAndroid : IDBConnection
    {
        //get connection to sqLiteDatabse
        public SQLiteConnection GetConnection()
        {
            var fileName = "ResDB.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);

            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);

            return connection;
        }
    }
}