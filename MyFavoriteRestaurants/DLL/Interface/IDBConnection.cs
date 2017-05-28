using SQLite.Net;

namespace DLL.Interface
{
    //interface SQLiteConnection
    public interface IDBConnection
    {
        SQLiteConnection GetConnection();
    }
}
