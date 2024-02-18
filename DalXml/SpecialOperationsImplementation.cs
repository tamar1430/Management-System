namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class SpecialOperationsImplementation : ISpecialOperations
{
    public DateTime? GetFinishProjectDate()
    {
        return Config.FinishProjectDate;
    }

    public DateTime? GetStartProjectDate()
    {
        return Config.StartProjectDate;
    }

    public void SetFinishProjectDate(DateTime value)
    {
        Config.FinishProjectDate = value;
    }

    public void SetStartProjectDate(DateTime value)
    {
        Config.StartProjectDate = value;
    }

    public void Reset()
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        engineers.Clear();
        XMLTools.SaveListToXMLSerializer(engineers, "engineers");

        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        tasks.Clear();
        XMLTools.SaveListToXMLSerializer(tasks, "tasks");

        List<Dependency> dependencys = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencys");
        dependencys.Clear();
        XMLTools.SaveListToXMLSerializer(dependencys, "dependencys");

        XElement doc = XElement.Load(@"..\xml\data-config.xml");
        doc!.Element("NextTaskId")?.SetValue(Convert.ToString(1) ?? throw new Exception("finish project date can`t be null"));
        doc!.Element("NextDependencyId")?.SetValue(Convert.ToString(1) ?? throw new Exception("finish project date can`t be null"));
        doc.Save(@"..\xml\data-config.xml");
    }

}
