using BlApi;
using DalApi;
namespace BlTest;

//ToString לעשות 🥇🥇🥇🥇
//הערות
//לו"ז פרויקט

internal class Program
{
    static readonly IBl s_bl = BlApi.Factory.Get(); //stage 4
    static readonly IDal s_dal = DalApi.Factory.Get; //stage 4

    /// <summary>
    /// this function initializes the program by calling the Do method of the Initialization class and then displays the options menu to the user. 
    /// It also handles any exceptions that might occur during program execution by catching them and displaying the exception message.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        try
        {
            //foreach (var item in s_dal.Task.ReadAll())
            //{
            //    s_dal.Task.Update(item with { RequiredEffortTime = new TimeSpan(10, 2, 3) });
            //}            //s_dal.Task.Create(new DO.Task() { RequiredEffortTime=new TimeSpan(10,0,0)});
                         //string a = s_bl.Task.Read(10).ToString();
                         // Console.WriteLine(s_bl.Task.Read(5));
                         //s_bl.Milestone.CreatingProjectSchedule(DateTime.Now,DateTime.Now);
                         //Console.WriteLine(new DateTime(2024,12,25));
                         //s_bl.Milestone.Read(4);
                         //var a = s_bl.Task.ReadAll().ToList();
                         //foreach (var item in a)
                         //{
                         //    Console.WriteLine(item);
                         //}
            //s_bl.Milestone.CreatingProjectSchedule(DateTime.Now, DateTime.Now + new TimeSpan(111, 23, 44));
            //s_bl.Task.ReadAll().ToList().ForEach(t=> Console.WriteLine(t));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}