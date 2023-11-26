
namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// create new dependency
    /// </summary>
    /// <param name="dependency"></param>
    /// <returns>id of the new dependency</returns>
    public int Create(Dependency dependency)
    {
        int newId = Config.NextDependencyId;
        Dependency newDependency = dependency with { Id = newId };
        XElement doc = XMLTools.LoadListFromXMLElement("dependencys");
        doc.Add(new XElement("Dependency",
                                        new XAttribute("Id", newDependency.Id),
                                        new XAttribute("DependentTask", newDependency.DependentTask),
                                        new XAttribute("PreviousTask", newDependency.PreviousTask)));
        XMLTools.SaveListToXMLElement(doc, "dependencys");
        return newId;
    }

    /// <summary>
    /// delete dependency
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        XElement? doc = XMLTools.LoadListFromXMLElement("dependencys");
        var dependency = doc.Descendants("Dependency")
                    .Where(dependency => Convert.ToInt32(dependency.Attribute("Id")!.Value).Equals(id))
                    .ToList().FirstOrDefault();
        if (dependency == null)
            throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
        else
            dependency.Remove();
        XMLTools.SaveListToXMLElement(doc, "dependencys");
    }

    /// <summary>
    /// read dependency
    /// </summary>
    /// <param name="id"></param>
    /// <returns>dependency with the if that recived</returns>
    public Dependency? Read(int id)
    {
        XElement? doc = XMLTools.LoadListFromXMLElement("dependencys");
        XElement? dependencyD = doc.Elements("Dependency")
                    .FirstOrDefault(dependency => Convert.ToInt32(dependency.Attribute("Id")!.Value).Equals(id));
        if (dependencyD != null)
        {
            Dependency dependency = new(Convert.ToInt32(dependencyD.Attribute("Id")!.Value),
                Convert.ToInt32(dependencyD.Attribute("DependentTask")!.Value),
                Convert.ToInt32(dependencyD.Attribute("PreviousTask")!.Value));
            return dependency;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Reads dependency that meet a certain condition
    /// </summary>
    /// <param name="filter">Pointer to a boolean function, delegate of type Func</param>
    /// <returns></returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement? doc = XMLTools.LoadListFromXMLElement("dependencys");
        Dependency? dependency = doc.Elements("Dependency")
               .Select(d =>
               {
                   Dependency dependencyD = new Dependency(Convert.ToInt32(d.Attribute("Id")!.Value),
                   Convert.ToInt32(d.Attribute("DependentTask")!.Value),
                   Convert.ToInt32(d.Attribute("PreviousTask")!.Value));
                   return dependencyD;
               })
            .FirstOrDefault(d => filter(d));
            return dependency;
    }

    /// <summary>
    /// Read all dependencys or dependencys that meet a certain condition
    /// </summary>
    /// <returns>dependencys list</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        XElement? doc = XMLTools.LoadListFromXMLElement("dependencys");
        List<Dependency> dependencys;
        if (filter != null)
        {
            dependencys = doc.Elements("Dependency")
                   .Select(d =>
                   {
                       Dependency dependency = new Dependency(Convert.ToInt32(d.Attribute("Id")!.Value),
                       Convert.ToInt32(d.Element("DependentTask")!.Value),
                       Convert.ToInt32(d.Element("PreviousTask")!.Value));
                       return dependency;
                   })
                  .Where(d => filter(d)).ToList();
        }
        else
        {
            dependencys = doc.Elements("Dependency")
                              .Select(d =>
                              {
                                  Dependency dependency = new Dependency(Convert.ToInt32(d.Attribute("Id")!.Value),
                                  Convert.ToInt32(d.Attribute("DependentTask")!.Value),
                                  Convert.ToInt32(d.Attribute("PreviousTask")!.Value));
                                  return dependency;
                              }).ToList();
        }
        return dependencys;
    }

    /// <summary>
    /// update dependency
    /// </summary>
    /// <param name="newDependency"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Dependency dependency)
    {
        XElement? doc = XMLTools.LoadListFromXMLElement("dependencys");
        //get the previous dependency
        var previousDependency = doc.Descendants("Dependency")
                    .Where(d => Convert.ToInt32(d.Attribute("Id")?.Value).Equals(dependency.Id))
                    .ToList().FirstOrDefault();

        // throw Exception if the dependency don't found 
        if (previousDependency == null)
            throw new DalDoesNotExistException($"Dependency with ID={dependency.Id} does Not exist");

        else
        {
            //remove the previous dependency
            previousDependency.Remove();
            //add the update dependency
            doc.Add(new XElement("Dependency",
                                            new XAttribute("Id", dependency.Id),
                                            new XAttribute("DependentTask", dependency.DependentTask),
                                            new XAttribute("PreviousTask", dependency.PreviousTask)));
            XMLTools.SaveListToXMLElement(doc, "dependencys");
        }
    }
}
