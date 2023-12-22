
namespace Dal;

using DO;
using System.Xml.Linq;

/// <summary>
/// class Config for Running ID number and more
/// </summary>
internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static int NextDependencyId
    {
        get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId");
    }
    internal static DateTime? startProjectDate
    {
        get => startProjectDate;
        set
        {
            XElement doc = XElement.Load(@"..\xml\data-config.xml");
            doc!.Element("startProjectDate")?.SetValue(Convert.ToString(value) ?? throw new Exception("start project date can`t be null"));
            doc.Save(@"..\xml\data-config.xml");
        }
    }
    internal static DateTime? finishProjectDate
    {
        get => finishProjectDate;
        set
        {
            XElement doc = XElement.Load(@"..\xml\data-config.xml");
            doc!.Element("finishProjectDate")?.SetValue(Convert.ToString(value)??throw new Exception("start project date can`t be null"));
            doc.Save(@"..\xml\data-config.xml");
        }
    }
}

