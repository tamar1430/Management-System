

namespace DalApi;
using DO;

/// <summary>
/// interface for Task
/// </summary>
public interface ITask
{
    int Create(Task task); //Creates new entity object in DAL
    Task? Read(int id); //Reads entity object by its ID
    List<Task> ReadAll(); //stage 1 only, Reads all entity objects
    void Update(Task newTask); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
}
