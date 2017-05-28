using System.Collections.ObjectModel;

namespace DLL.Interface
{
    //interface for Respositories
    public interface IRespository<T>
    {
        T Create(T t);

        T Read(int id);

        ObservableCollection<T> Read();

        T Update(T t);

        bool Delete(int id);

       
    }
}
