
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
    internal static DateTime? StartProjectDate
    {
        get
        {
            XElement doc = XElement.Load(@"..\xml\data-config.xml");
            //var a = doc!.Element("StartProjectDate") != null ? (DateTime)doc!.Element("StartProjectDate")! : null;
            //return a;
            return doc!.Element("StartProjectDate")!.Value != null ? DateTime.Parse(doc!.Element("StartProjectDate")!.Value) : null;
        }
        set
        {
            XElement doc = XElement.Load(@"..\xml\data-config.xml");
            doc!.Element("StartProjectDate")?.SetValue(Convert.ToString(value) ?? throw new Exception("start project date can`t be null"));
            doc.Save(@"..\xml\data-config.xml");
        }
    }
    internal static DateTime? FinishProjectDate
    {
        get
        {
            XElement doc = XElement.Load(@"..\xml\data-config.xml");
            return doc!.Element("FinishProjectDate")!.Value != null ? DateTime.Parse(doc!.Element("FinishProjectDate")!.Value) : null;
        }
        set
        {
            XElement doc = XElement.Load(@"..\xml\data-config.xml");
            doc!.Element("FinishProjectDate")?.SetValue(Convert.ToString(value)??throw new Exception("finish project date can`t be null"));
            doc.Save(@"..\xml\data-config.xml");
        }
    }
}

