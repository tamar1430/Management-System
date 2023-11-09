using Dal;
using DalApi;
using DO;
using System.Threading.Tasks;


namespace DalTest;

internal class Program
{
    private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1

    static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
            ShowOptionsMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private static void ShowOptionsMenu()
    {
        bool exitMenu = false;
        while (!exitMenu)
        {
            Console.WriteLine("Welcome to the Options Menu!");
            Console.WriteLine("0: Exit");
            Console.WriteLine("1: Engineer");
            Console.WriteLine("2: Task");
            Console.WriteLine("3: Dependency");

            int userInput = int.Parse(Console.ReadLine()!);
            switch (userInput)
            {
                case 0:
                    Console.WriteLine("Exiting the program...");
                    exitMenu = true;
                    break;
                case 1:
                    Console.WriteLine("You selected Engineer.");
                    menuEntity("Engineer");
                    // Perform engineer-related actions
                    break;
                case 2:
                    Console.WriteLine("You selected Task.");
                    menuEntity("Task");
                    // Perform task-related actions
                    break;
                case 3:
                    Console.WriteLine("You selected Dependency.");
                    menuEntity("Dependency");
                    // Perform dependency-related actions
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    ShowOptionsMenu();
                    break;
            }
        }

    }


    public static void menuEntity(string entity)
    {
        bool exitMenu = false;

        while (!exitMenu)
        {
            Console.WriteLine($"---- {entity} Menu ----");
            Console.WriteLine("1: Create");
            Console.WriteLine("2: Read (by ID)");
            Console.WriteLine("3: Read All");
            Console.WriteLine("4: Update");
            Console.WriteLine("5: Delete");
            Console.WriteLine("0: Exit");

            int choice = (int.Parse(Console.ReadLine()!));

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Exiting the program...");
                    exitMenu = true;
                    break;
                case 1:
                    Create(entity);
                    break;
                case 2:
                    Read(entity);
                    break;
                case 3:
                    ReadAll(entity);
                    break;
                case 4:
                    Update(entity);
                    break;
                case 5:
                    Delete(entity);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }


    private static void Create(string entity)
    {


        Console.WriteLine($"---- Create {entity}  ----");

        switch (entity)
        {
            case "Engineer":
                CreateEngineer();
                break;
            case "Task":
                CreateTask();
                break;
            case "Dependency":
                CreateDependency();
                break;
        }
    }

    private static void CreateEngineer()
    {
        try
        {
            Console.WriteLine("id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("name: ");
            string name = Console.ReadLine();
            Console.WriteLine("email: ");
            string email = Console.ReadLine();
            Console.WriteLine("leval: ");
            EngineerExperience level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine());
            Console.WriteLine("cost: ");
            double cost = double.Parse(Console.ReadLine());

            Engineer newEngineer = new(id, name, email, level, cost);

            s_dalEngineer!.Create(newEngineer);

            Console.WriteLine("Entity created successfully.");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); };

    }

    private static void CreateTask()
    {
        Console.WriteLine("description: ");
        string description = Console.ReadLine();
        Console.WriteLine("alias: ");
        string alias = Console.ReadLine();
        Console.WriteLine("isMilestone: ");
        bool isMilestone = bool.Parse(Console.ReadLine());
        Console.WriteLine("copmlexityLevel: ");
        EngineerExperience copmlexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine());
        Console.WriteLine("createdAtDate: "); 
        DateTime createdAtDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("scheduledDate: ");
        DateTime scheduledDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("startDate: ");
        DateTime startDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("foresastDate: ");
        TimeSpan foresastDate = TimeSpan.Parse(Console.ReadLine());
        Console.WriteLine("deadLineDate: ");
        DateTime deadLineDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("completeDate: ");
        DateTime completeDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("deliverable: ");
        string deliverable = Console.ReadLine();
        Console.WriteLine("remarks: ");
        string remarks = Console.ReadLine();
        Console.WriteLine("engineerld: ");
        int? engineerld = int.Parse(Console.ReadLine());

        DO.Task newTask = new(0, description, alias, isMilestone, copmlexityLevel, createdAtDate, scheduledDate,
            startDate, foresastDate, deadLineDate, completeDate, deliverable, remarks, engineerld);

        s_dalTask!.Create(newTask);

        Console.WriteLine("Entity created successfully.");
    }

    private static void CreateDependency()
    {
        int dependentTask = int.Parse(Console.ReadLine());
        int previousTask = int.Parse(Console.ReadLine());

        Dependency newDependency = new(0, dependentTask, previousTask);

        s_dalDependency!.Create(newDependency);

        Console.WriteLine("Entity created successfully.");
    }

    private static void Read(string entity)
    {
        Console.WriteLine("---- Read by ID ----");
        Console.Write($"Enter the ID of the {entity} you want to display: ");
        int id = int.Parse(Console.ReadLine());
        switch (entity)
        {
            case "Engineer":
                Console.WriteLine(s_dalEngineer!.Read(id));
                break;
            case "Task":
                Console.WriteLine(s_dalTask!.Read(id));
                break;
            case "Dependency":
                Console.WriteLine(s_dalDependency!.Read(id));
                break;
        }
    }

    /// <summentityry>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    private static void ReadAll(string entity)
    {
        Console.WriteLine($"---- Read All {entity}s ----");
        switch (entity)
        {
            case "Engineer":
                foreach (Engineer engineer in s_dalEngineer!.ReadAll())
                {
                    Console.WriteLine(engineer);
                }
                break;
            case "Task":
                foreach (DO.Task task in s_dalTask!.ReadAll())
                {
                    Console.WriteLine(task);
                }
                break;
            case "Dependency":
                foreach (Dependency dependency in s_dalDependency!.ReadAll())
                {
                    Console.WriteLine(dependency);
                }
                break;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    private static void Update(string entity)
    {
        try
        {
            Console.WriteLine($"---- Update {entity}  ----");

            switch (entity)
            {
                case "Engineer":
                    UpdateEngineer();
                    break;
                case "Task":
                    UpdateTask();
                    break;
                case "Dependency":
                    UpdateDependency();
                    break;
            }
            Console.WriteLine("Entity updated successfully.");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); };
    }

    private static void UpdateEngineer()
    {
        Console.Write($"Enter the ID of the engineer you want to update: ");
        int id = int.Parse(Console.ReadLine());

        Console.WriteLine(s_dalEngineer!.Read(id));

        Console.WriteLine("name: ");
        string name = Console.ReadLine()! ?? s_dalEngineer!.Read(id).Name;
        Console.WriteLine("email: ");
        string email = Console.ReadLine()! ?? s_dalEngineer!.Read(id).Email;
        Console.WriteLine("leval: ");
        EngineerExperience level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
        if (level == null)
        { level = s_dalEngineer!.Read(id).Level; }
        Console.WriteLine("cost: ");
        double? cost = double.Parse(Console.ReadLine()!);

        Engineer newEngineer = new(id, name, email, level, cost ?? s_dalEngineer!.Read(id).Cost);

        s_dalEngineer!.Update(newEngineer);
    }

    private static void UpdateTask()
    {
        Console.Write($"Enter the ID of the engineer you want to update: ");
        int id = int.Parse(Console.ReadLine());

        Console.WriteLine(s_dalEngineer!.Read(id));

        Console.WriteLine("description: ");
        string description = Console.ReadLine()! ?? s_dalTask!.Read(id).Description;

        Console.WriteLine("alias: ");
        string alias_ = Console.ReadLine()! ?? s_dalTask!.Read(id).Alias;

        Console.WriteLine("isMilestone: ");
        bool isMilestone = bool.Parse(Console.ReadLine()!);
        if (isMilestone == null)
        { isMilestone = s_dalTask!.Read(id).IsMilestone; };

        Console.WriteLine("copmlexityLevel: ");
        EngineerExperience copmlexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine());
        if (copmlexityLevel == null)
        { copmlexityLevel = s_dalTask!.Read(id).CopmlexityLevel; };

        Console.WriteLine("createdAtDate: ");
        DateTime createdAtDate = DateTime.Parse(Console.ReadLine()!);
        if (createdAtDate == null) { createdAtDate = (DateTime)s_dalTask!.Read(id).CreatedAtDate; };

        Console.WriteLine("scheduledDate: ");
        DateTime scheduledDate = DateTime.Parse(Console.ReadLine()!);
        if (scheduledDate == null) { scheduledDate = (DateTime)s_dalTask!.Read(id).ScheduledDate; };

        Console.WriteLine("startDate: ");
        DateTime startDate = DateTime.Parse(Console.ReadLine()!);
        if (scheduledDate == null) { startDate = (DateTime)s_dalTask!.Read(id).StartDate; };

        Console.WriteLine("foresastDate: ");
        TimeSpan foresastDate = TimeSpan.Parse(Console.ReadLine()!);
        if (foresastDate == null) { foresastDate = (TimeSpan)s_dalTask!.Read(id).ForesastDate; };

        Console.WriteLine("deadLineDate: ");
        DateTime deadLineDate = DateTime.Parse(Console.ReadLine()!);
        if (deadLineDate == null) { deadLineDate = (DateTime)s_dalTask!.Read(id).DeadLineDate; };

        Console.WriteLine("completeDate: ");
        DateTime completeDate = DateTime.Parse(Console.ReadLine()!);
        if (completeDate == null) { completeDate = (DateTime)s_dalTask!.Read(id).CompleteDate; };

        Console.WriteLine("deliverable: ");
        string deliverable = Console.ReadLine()! ?? s_dalTask!.Read(id).Deliverable;

        Console.WriteLine("remarks: ");
        string remarks = Console.ReadLine()! ?? s_dalTask!.Read(id).Remarks;

        Console.WriteLine("engineerld: ");
        int engineerld = int.Parse(Console.ReadLine()!);
        if (engineerld == null) { s_dalTask!.Read(id); }


        DO.Task newTask = new(id, description, alias_,
            isMilestone, copmlexityLevel, createdAtDate, startDate,
            scheduledDate, foresastDate, deadLineDate,
            completeDate, deliverable, remarks, engineerld);


        s_dalTask!.Update(newTask);
    }



    private static void UpdateDependency()
    {
        Console.Write($"Enter the ID of the dependency you want to update: ");
        int id = int.Parse(Console.ReadLine());

        Console.WriteLine(s_dalDependency!.Read(id));

        Console.WriteLine("dependentTask: ");
        int dependentTask = int.Parse(Console.ReadLine()!);
        if (dependentTask == null) { dependentTask = s_dalDependency!.Read(id).DependentTask; };
        Console.WriteLine("previousTask: ");
        int previousTask = int.Parse(Console.ReadLine()!);
        if (previousTask == null) { dependentTask = s_dalDependency!.Read(id).PreviousTask; };

        Dependency newDependency = new(id, dependentTask, previousTask);

        s_dalDependency!.Update(newDependency);
    }

    private static void Delete(string entity)
    {
        try
        {
            Console.WriteLine($"---- Delete {entity}  ----");
            Console.Write($"Enter the ID of the {entity} you want to delete: ");
            int id = int.Parse(Console.ReadLine());

            switch(entity)
            {
                case "Engineer":
                    s_dalEngineer!.Delete(id);
                    break;
                case "Task":
                    s_dalTask!.Delete(id);
                    break;
                case "Dependency":
                    s_dalDependency!.Delete(id);
                    break;

            }
            

            Console.WriteLine("Entity deleted successfully.");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); };
    }
}
