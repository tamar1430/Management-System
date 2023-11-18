
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
        Engineer? engineer1 = (from engineer in DataSource.Engineers
                  where engineer.Id == newEngineer.Id
                  select engineer).ToList().FirstOrDefault();
        if (engineer1 != null)
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
        Engineer? engineer = (from engineer1 in DataSource.Engineers
                             where engineer1.Id == id
                             select engineer1).ToList().FirstOrDefault();
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
        return (from engineer in DataSource.Engineers
                where engineer.Id == id
                select engineer).ToList().FirstOrDefault();
    }

    /// <summary>
    /// Reads engineer that meet a certain condition
    /// </summary>
    /// <param name="filter">Pointer to a boolean function, delegate of type Func</param>
    /// <returns></returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.Where(filter).FirstOrDefault();
    }

    /// <summary>
    /// Read all engineers or engineers that meet a certain condition
    /// </summary>
    /// <returns>the engineers list</returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Engineers.Select(engineer => engineer);
        else
            return DataSource.Engineers.Where(filter);
    }

    /// <summary>
    /// update engineer
    /// </summary>
    /// <param name="engineer"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Engineer engineer)
    {
        Engineer? previousEngineer = (from engineer1 in DataSource.Engineers
                                      where engineer.Id == engineer.Id
                                      select engineer).ToList().FirstOrDefault();

        if (previousEngineer == null)
            throw new DalDoesNotExistException($"Engineer with ID={engineer.Id} does Not exist");
        DataSource.Engineers.Remove(previousEngineer);
        DataSource.Engineers.Add(engineer);
    }
}
