
namespace Dal;
using DalApi;
using DO;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer newEngineer)
    {
        if (DataSource.Engineers.Find(engineer => engineer.Id == newEngineer.Id) != null)
            throw new Exception("Engineer already exists an object of type with the same Id");
        DataSource.Engineers.Add(newEngineer);
        return newEngineer.Id;
    }

    public void Delete(int id)
    {
        Engineer? engineer = DataSource.Engineers.Find(Engineer => Engineer.Id == id);
        if (engineer == null)
            throw new Exception($"Engineer with ID={id} does Not exist");
        else
            DataSource.Engineers.Remove(engineer);
    }

    public Engineer? Read(int id)
    {
        Engineer? a= DataSource.Engineers.Find(Engineer => Engineer.Id == id);
        return a;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer engineer)
    {
        Engineer? previousEngineer = DataSource.Engineers.Find(Engineer => Engineer.Id == engineer.Id);
        if (previousEngineer == null)
            throw new Exception($"Engineer with ID={engineer.Id} does Not exist");
        DataSource.Engineers.Remove(previousEngineer);
        DataSource.Engineers.Add(engineer);
    }
}