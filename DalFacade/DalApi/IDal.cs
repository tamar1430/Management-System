namespace DalApi;

public interface IDal
{
    IEngineer Engineer { get; }
    ITask Task { get; }
    IDependency Dependency { get; }
    //DateTime StartProjectDate
    //{
    //    get
    //    {
    //        if()
    //    }
    //    set
    //    {

    //        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml");
    //        dalConfig?.Element("dal")?.Value == "xml"?
            
    //    }
    //}
}
