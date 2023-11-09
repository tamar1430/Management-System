
namespace Dal;
using DalApi;
using DO;


internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// create new engineer
    /// </summary>
    /// <param name="newEngineer"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Create(Engineer newEngineer)
    {
        if (DataSource.Engineers.Find(engineer => engineer.Id == newEngineer.Id) != null)
            throw new DalAlreadyExistsException("Engineer already exists an object of type with the same Id");
        DataSource.Engineers.Add(newEngineer);
        return newEngineer.Id;
    }

    /// <summary>
    /// delete engineer
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        Engineer? engineer = DataSource.Engineers.Find(Engineer => Engineer.Id == id);
        if (engineer == null)
            throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist");
        else
            DataSource.Engineers.Remove(engineer);
    }

    /// <summary>
    /// read engineer
    /// </summary>
    /// <param name="id"></param>
    /// <returns>engineer with the id received</returns>
    public Engineer? Read(int id)
    {
        Engineer? a= DataSource.Engineers.Find(Engineer => Engineer.Id == id);
        return a;
    }

    /// <summary>
    /// read all the engineers
    /// </summary>
    /// <returns>the engineers list</returns>
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    /// <summary>
    /// update engineer
    /// </summary>
    /// <param name="engineer"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Engineer engineer)
    {
        Engineer? previousEngineer = DataSource.Engineers.Find(Engineer => Engineer.Id == engineer.Id);
        if (previousEngineer == null)
            throw new DalDoesNotExistException($"Engineer with ID={engineer.Id} does Not exist");
        DataSource.Engineers.Remove(previousEngineer);
        DataSource.Engineers.Add(engineer);
    }
}