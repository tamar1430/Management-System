using BlApi;
using DalApi;
namespace BlTest;
using BlApi;
using BO;
using DalApi;
using DO;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml.Linq;
internal class Program
{
    private static readonly Random s_rand = new();

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
               // DalTest.Initialization.DO;

            DisplayMainMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        static void DisplayMainMenu()
        {

            bool exitTheMenu = false;

            while (!exitTheMenu)
            {
                Console.WriteLine("Main Menu - Select an entity you want to check:");
                Console.WriteLine("0. Exiting the main menu");
                Console.WriteLine("1. Task");
                Console.WriteLine("2. Engineer");
                Console.WriteLine("3. Milestone");
                Console.Write("Enter your choice: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        exitTheMenu = true;
                        break;
                    case "1":
                     
                        DisplaySubMenu("Task");

                        break;
                    case "2":
                    
                        DisplaySubMenu("Engineer");

                        break;
                    case "3":
                        
                        DisplayMilstonMenu("Milestone");

                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }

        }
    }

    public static void DisplaySubMenu(string entityName)
    {
        try
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Console.WriteLine("Select the method you want to execute:");
                Console.WriteLine("1. Exiting the main menu");
                Console.WriteLine("2. Adding a new object to the " + entityName + " list");
                Console.WriteLine("3. Object display by identifier");
                Console.WriteLine("4. Updating existing object data");
                Console.WriteLine("5. The display of the list of all " + entityName);
                Console.WriteLine("6. Deleting an existing object from a list");

                Console.Write("Enter your choice: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        exitMenu = true;
                        break;
                    case "2":
                       
                        createObject(entityName);
                        break;
                    case "3":
                       
                        readObject(entityName);
                        break;
                    case "4":
                   
                        updateObject(entityName);
                        break;
                    case "5":
                     
                         readAllObjects(entityName);
                        break;
                    case "6":
                      
                          deleteObject(entityName);
                       
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
        catch (Exception ex) { Console.WriteLine(ex); }
    }

    public static void DisplayMilstonMenu(string entityName)
    {
        try
        {
            bool exitMenu = false;

            while (!exitMenu)
            {

                Console.WriteLine("5. The display of the list of all Milston");
                Console.WriteLine("6. Deleting an existing object from a list");
                Console.Write("Enter your choice: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        exitMenu = true;
                        break;
                    case "2":

                        createObject(entityName);
                        break;
                    case "3":

                        readObject(entityName);
                        break;
                    case "4":
                        updateObject(entityName);
                        break;
   
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
        catch (Exception ex) { Console.WriteLine(ex); }

    }


    public static void createObject(string entityName)
    {
        switch (entityName)
        {
            case "Task":
                createTask();
                break;
            case "Engineer":
                createEngineer();
                break;
            case "Milestone":
                throw (new Exception("Cannot create an object of Milestone"));

            default:
                throw (new Exception("no such entity name"));
        }

    }
    
    static void createTask()///A function that receives data from the user for the task and creates a new task
    {
        string description = GetInput("Enter the task description: ");
        string alias = GetInput("Enter the task alias: ");
        DateTime startDate = GetDateTimeInput("Enter the task start date and time (YYYY-MM-DD HH:mm:ss): ");
        DateTime deadLine = GetDateTimeInput("Enter the task deadline date and time (YYYY-MM-DD HH:mm:ss): ");
        DateTime createdAtDate = DateTime.Now.AddDays(-s_rand.Next(0, 100));
        string deliverables = GetInput("Enter the task deliverables: ");
        string remarks = GetInput("Enter the task Remarks: ");
        int EngineerId = GetIntInput("Enter the engineers Id ");
        string EngineerName = GetInput("Enter the engineers name : ");
        BO.EngineerExperience copmlexityLevel = GetCopmlexityLevelInput("Please enter the engineer's experience level (  Novice, AdvancedBeginner, Competent, Proficient, Expert): ");
        BO.Task task = new BO.Task
        {
            Description = description,
            Alias = alias,
            CreatedAtDate = createdAtDate,
            Status = 0,
            StartDate = startDate,
            DeadLine = deadLine,
            Deliverable = deliverables,
            Remarks = remarks,
            Engineer = new EngineerInTask { Id = EngineerId, Name = EngineerName },
            CopmlexityLevel = copmlexityLevel
        };
        try
        {

            s_bl!.Task.Create(task);
        }
        catch (Exception e) { Console.WriteLine(e); }
    }
    
    static void createEngineer()///A function that receives data from the user for the Engineer and creates a new Engineer
    {
        string name = GetInput("Please enter the engineer's name: ");
        string email = GetInput("Please enter the engineer's email: ");
        BO.EngineerExperience level = GetCopmlexityLevelInput("Please enter the engineer's experience level (  Novice, AdvancedBeginner, Competent, Proficient, Expert): \"");
        double cost = Convert.ToDouble(GetInput("Please enter the engineer's cost per hour: "));
        int id;
        int.TryParse(GetInput("Please enter the engineer's ID: "), out id);
        BO.Engineer engineer = new BO.Engineer { Id = id, Name = name, Email = email, Level = level, Cost = cost };
        try
        {

            s_bl!.Engineer.Create(engineer);
        }
        catch (Exception e) { Console.WriteLine(e); }
    }

    public static void readObject(string entityName)
    {
        switch (entityName)
        {
            case "Task":
                readTask();
                break;
            case "Engineer":
                readEngineer();
                break;
            case "Milestone":
                readMilestone();
                break;
            default:
                throw (new Exception("no such entity name"));
        }

    }

    static void readTask()///Receives from the Task id and if it exists it prints it
    {
        int idTask;
        int.TryParse((GetInput("Enter the Tasks id: ")), out idTask);
        BO.Task? taskRead = s_bl!.Task.Read(idTask);
        Console.WriteLine(taskRead);

    }

    static void readEngineer()///Receives from the Engineer id and if it exists it prints it
    {
        int idEngineer;
        int.TryParse(GetInput("Enter the Engineer id: "), out idEngineer);
        BO.Engineer? engineerRead = s_bl!.Engineer.Read(idEngineer);
        Console.WriteLine(engineerRead);
    }

    public static void readAllObjects(string entityName)
    {
        switch (entityName)
        {
            case "Task":
                readAllTasks();
                break;
            case "Engineer":
                readAllEngineers();
                break;
            default:
                throw (new Exception("no such entity name"));
        }

    }

    static void readAllTasks()///Prints all entities of Task
    {
        List<BO.Task?> taskList = s_bl!.Task.ReadAll(null).ToList();
        foreach (var task in taskList)
        {
            Console.WriteLine(task);
        }
    }
    
    static void readAllEngineers()
    {
        List<BO.Engineer?> engineerList = s_bl!.Engineer.ReadAll().ToList();
        foreach (var engineer in engineerList)
        {
            Console.WriteLine(engineer);
        }
    }

    public static void deleteObject(string entityName)
    {
        switch (entityName)
        {
            case "Task":
                deleteTask();
                break;
            case "Engineer":
                deleteEngineer();
                break;
            default:
                throw (new Exception("no such entity name"));
        }

    }

    static void deleteTask()///Gets the id from the Tasks and deletes it if it exists
    {
        try
        {
            int idTask;
            int.TryParse(GetInput("Enter the Tasks id: "), out idTask);
            s_bl!.Task.Delete(idTask);
        }
        catch (Exception e) { Console.WriteLine(e); }

    }
    static void deleteEngineer()///Gets the id from the Engineer and deletes it if it exists
    {
        try
        {
            int idEngineer;
            int.TryParse(GetInput("Enter the Engineer id: "), out idEngineer);
            s_bl!.Engineer.Delete(idEngineer);
        }
        catch (Exception e) { Console.WriteLine(e); }

    }

    public static void updateObject(string entityName)
    {
        switch (entityName)
        {
            case "Task":
                updateTask();
                break;
            case "Engineer":
                updateEngineer();
                break;
            case "Milestone":
                updateMilestone();
                break;
            default:
                throw (new Exception("no such entity name"));
        }

    }
   
    static void updateTask() 
    {
        int idTask;
        int.TryParse(GetInput("Enter the Tasks id: "), out idTask);
        BO.Task? task = s_bl!.Task.Read(idTask);
        if (task != null)
        {
            Console.WriteLine(task);
            Console.WriteLine("Enter Task Data:");
            string? description = GetInput("Enter the task description: ");
            string? alias = GetInput("Enter the task alias: ");
            DateTime? start = GetNullDatTimeInput("Enter the task start date and time (YYYY-MM-DD HH:mm:ss): ");
            DateTime? scheduled = GetNullDatTimeInput("Enter the task scheduled date and time (YYYY-MM-DD HH:mm:ss): ");
            DateTime? forecas = GetNullDatTimeInput("Enter the task Enter the task forecas date and time (YYYY-MM-DD HH:mm:ss): ");
            DateTime? deadline = GetNullDatTimeInput("Enter the task deadline date and time (YYYY-MM-DD HH:mm:ss): ");
            DateTime? complete = GetNullDatTimeInput("Enter the task Enter the task complete date and time (YYYY-MM-DD HH:mm:ss): ");
            string? deliverables = GetInput("Enter the task deliverables: ");
            string? remarks = GetInput("Enter Remarks: ");
            int engineerId = GetIntInput("Enter Engineer ID: ");
            BO.EngineerExperience? complexityLevel = GetCopmlexityLevelInput("Please enter the engineer's experience level (  Novice, AdvancedBeginner, Competent, Proficient, Expert): ");
            string EngineerName = GetInput("Enter the engineers name : ");
            BO.Task updatedTask = new BO.Task
            {
                Id = task.Id,
                Description = description != "" ? description : task.Description,
                Alias = alias != "" ? alias : task.Alias,
                Status = GetStatusInput("enter the status "),
                StartDate = start ?? task.StartDate,
                ScheduledStartDate = scheduled ?? task.ScheduledStartDate,
                ForecastDate = forecas ?? task.ForecastDate,
                CompleteDate = complete ?? task.CompleteDate,
                DeadLine = deadline ?? task.DeadLine,
                Deliverable = deliverables != "" ? deliverables : task.Deliverable,
                Remarks = remarks != "" ? remarks : task.Remarks,
                Engineer = new EngineerInTask { Id = engineerId, Name = EngineerName },
                CopmlexityLevel = complexityLevel ?? task.CopmlexityLevel,
            };
            s_bl!.Task.Update(updatedTask);

        }

    }
    static void updateEngineer()
    {
        int idEngineer;
        int.TryParse(GetInput("Enter the Engineer id: "), out idEngineer);
        BO.Engineer? engineer = s_bl!.Engineer.Read(idEngineer);
        if (engineer != null)
        {
            Console.WriteLine(engineer);
            string? name = GetInput("Please enter the engineer's name: ");
            string? email = GetInput("Please enter the engineer's email: ");
            BO.EngineerExperience? level = GetCopmlexityLevelInput("Please enter the engineer's experience level (  Novice, AdvancedBeginner, Competent, Proficient, Expert): ");
            double? cost = GetNullDoubleInput("Please enter the engineer's cost per hour: ");
            int? taskId = GetNullIntInput("Enter task Id: ");
            string alias = GetInput("Enter the alias  ");
            TaskInEngineer? taskinEngineer = new TaskInEngineer { Id = taskId ?? 0, Alias = alias };

            BO.Engineer updatedEngineer = new BO.Engineer
            {
                Id = engineer.Id,
                Name = name != "" ? name : engineer.Name,
                Email = email != "" ? email : engineer.Email,
                Level = level ?? engineer.Level,
                Cost = cost ?? engineer.Cost,
                Task = taskId == null ? null : taskinEngineer
            };
            s_bl!.Engineer.Update(updatedEngineer);
        }
    }

    static string GetInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    public static int? GetNullIntInput(string message)
    {
        int? inputMutable;
        string input = GetInput(message);
        bool success = int.TryParse(input, out int parsedValue);

        if (success)
        {
            inputMutable = parsedValue;
        }
        else
        {
            inputMutable = null;
        }
        return inputMutable;
    }
    public static int GetIntInput(string message)
    {

        int inputMutable;
        string input = GetInput(message);
        bool success = int.TryParse(input, out int parsedValue);

        if (success)
        {
            inputMutable = parsedValue;
        }
        else
        {
            throw new BlNullPropertyException("null input");
        }
        return inputMutable;
    }
    
    public static DateTime? GetNullDatTimeInput(string message)
    {
        DateTime? inputMutable;
        string? input = GetInput(message);
        bool success = DateTime.TryParse(input, out DateTime parsedValue);

        if (success)
        {
            inputMutable = parsedValue;
        }
        else
        {
            inputMutable = null;
        }
        return inputMutable;
    }
    
    public static double? GetNullDoubleInput(string message)///A function with a print-to-screen parameter that receives a double from the user and returns a variable with the content and if nothing is entered a null value
    {

        double? inputMutable;
        string? input = GetInput(message);
        bool success = double.TryParse(input, out double parsedValue);

        if (success)
        {
            inputMutable = parsedValue;
        }
        else
        {
            inputMutable = null;
        }
        return inputMutable;
    }

    static bool GetBoolInput(string message)
    {
        Console.Write(message);
        string? input = Console.ReadLine();
        return bool.Parse(input!);
    }

    static DateTime GetDateTimeInput(string message)    
    {
        Console.Write(message);

        DateTime createdAtDate;
        DateTime.TryParseExact(GetInput("Enter the created at date and time (yyyy-MM-dd HH:mm:ss): "), "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out createdAtDate);
        return createdAtDate;
    }
    static BO.EngineerExperience GetCopmlexityLevelInput(string message)
    {
        Console.Write(message);
        string? level = Console.ReadLine();
        BO.EngineerExperience experienceLevel;
        Enum.TryParse(level, out experienceLevel);
        return experienceLevel;
    }
    static BO.Status GetStatusInput(string message)
    {
        Console.Write(message);
        string? status = Console.ReadLine();
        BO.Status statusLevel;
        Enum.TryParse(status, out statusLevel);
        return statusLevel;
    }
    static void readMilestone()
    {
        int idMilestone;
        int.TryParse((GetInput("Enter the Milestone id: ")), out idMilestone);
        BO.Milestone? milestoneRead = s_bl!.Milestone.Read(idMilestone);
        Console.WriteLine(milestoneRead);

    }
    static void updateMilestone()
    {
        int idMilestone;
        int.TryParse((GetInput("Enter the Milestone id: ")), out idMilestone);
        Console.WriteLine(s_bl.Milestone.Read(idMilestone));
        Console.WriteLine("Enter Milestone Data:");
        string? remarks = GetInput("Enter Remarks: ");
        string? description = GetInput("Enter the Milestone description: ");
        string? alias = GetInput("Enter the Milestone alias: ");
        s_bl.Milestone.Update(idMilestone, alias, description, remarks);
    }

}