

namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

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
        List<Engineer> ? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
    Engineer? engineer = (from engineer1 in engineers
                          where engineer1.Id == newEngineer.Id
                          select engineer1).ToList().FirstOrDefault();
        if (engineer != null)
            throw new DalAlreadyExistsException("Engineer already exists an object of type with the same Id");
        engineers.Add(newEngineer);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, "engineers");
        return newEngineer.Id;

    }

    /// <summary>
    /// delete engineer
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = (from engineer1 in engineers
                              where engineer1.Id == id
                              select engineer1).ToList().FirstOrDefault();
        if (engineer == null)
            throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist");
        else
            engineers.Remove(engineer);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, "engineers");
    }

    /// <summary>
    /// read engineer
    /// </summary>
    /// <param name="id"></param>
    /// <returns>engineer with the id received</returns>
    public Engineer? Read(int id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return (from engineer in engineers
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
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return engineers.Where(filter).FirstOrDefault();
    }

    /// <summary>
    /// Read all engineers or engineers that meet a certain condition
    /// </summary>
    /// <returns>the engineers list</returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (filter == null)
            return engineers.Select(engineer => engineer);
        else
            return engineers.Where(filter);
    }

    /// <summary>
    /// update engineer
    /// </summary>
    /// <param name="engineer"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Engineer engineer)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? previousEngineer = (from engineer1 in engineers
                                      where engineer1.Id == engineer.Id
                                      select engineer1).ToList().FirstOrDefault();

        if (previousEngineer == null)
            throw new DalDoesNotExistException($"Engineer with ID={engineer.Id} does Not exist");
        engineers.Remove(previousEngineer);
        engineers.Add(engineer);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, "engineers");
    }
}
