
using System.Reflection;
using System.Text;

namespace BO;

public static class Tools
{
    public static Status status(DO.Task doTask)
    {
        return doTask.ScheduledDate is null ? BO.Status.Unscheduled :
                   doTask.StartDate is null ? BO.Status.Scheduled :
                   doTask.CompleteDate is null ?
                   (doTask.DeadLineDate < DateTime.Now ? BO.Status.OnTrack : BO.Status.InJeopardy) :
                   BO.Status.Done;
    }

    public static string ToStringProperty<T>(this T obj)
    {
        return ToStringProperty(obj, "");
    }

    private static string ToStringProperty(object obj, string indent)
    {
        var type = obj.GetType();
        var properties = type.GetProperties();
        var result = "";

        foreach (var property in properties)
        {
            var value = property.GetValue(obj);
            var formattedValue = "";

            if (value is IEnumerable<Object> enumerableValue && !(value is string))
            {
                formattedValue = $"{property.Name}: [";
                var innerIndent = indent + "\t";

                foreach (var item in enumerableValue)
                {
                    formattedValue += $"\n{innerIndent}{ToStringProperty(item, innerIndent)},";
                }

                formattedValue += $"\n{indent}\t]";
            }
            else if (value != null && value.GetType() != typeof(string) && !value.GetType().IsValueType)
            {
                formattedValue = $"{property.Name}: {ToStringProperty(value, indent + "\t")}";
            }
            else
            {
                formattedValue = $"{property.Name}: {value}";
            }

            result += $"\n{indent}{formattedValue}";
        }

        return result;
    }

}





