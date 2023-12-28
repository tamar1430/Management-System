namespace Dal;
using DalApi;
using DO;

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
    }

}
