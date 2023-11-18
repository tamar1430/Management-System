
namespace DalApi;

public interface ICrud<T> where T : class
{
    int Create(T item); //Creates new entity object in DAL
    T? Read(int id); //Reads entity object by its ID
    T? Read(Func<T, bool> filter);//Reads entity object that meet a certain condition
    IEnumerable<T?> ReadAll(Func<T, bool>? filter = null); // Read all entitys or entitys that meet a certain condition
    void Update(T item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
}
