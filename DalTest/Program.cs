using Dal;
using DalApi;
using DO;


namespace DalTest;

internal class Program
{
    private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1

    /// <summary>
    /// this function initializes the program by calling the Do method of the Initialization class and then displays the options menu to the user. 
    /// It also handles any exceptions that might occur during program execution by catching them and displaying the exception message.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
            ShowOptionsMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// this function presents an options menu to the user, allows them to select various entities (Engineer, Task, or Dependency), 
    /// and performs specific actions based on their selection.
    /// It also handles invalid input by displaying an error message and showing the options menu again
    /// </summary>
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
                    ShowMenuEntity("Engineer");
                    // Perform engineer-related actions
                    break;
                case 2:
                    Console.WriteLine("You selected Task.");
                    ShowMenuEntity("Task");
                    // Perform task-related actions
                    break;
                case 3:
                    Console.WriteLine("You selected Dependency.");
                    ShowMenuEntity("Dependency");
                    // Perform dependency-related actions
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    ShowOptionsMenu();
                    break;
            }
        }

    }

    /// <summary>
    /// this function defines a method that displays a menu for a specific entity and allows the user to perform various actions related to that entity. 
    /// It handles invalid input by displaying an error message and showing the entity menu again.
    /// </summary>
    /// <param name="entity"></param>
    private static void ShowMenuEntity(string entity)
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

    #region Create

    /// <summary>
    /// this function defines a method that creates an entity based on the provided entity parameter. 
    /// It dynamically calls a specific creation method based on the value of entity.
    /// </summary>
    /// <param name="entity"></param>
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

    /// <summary>
    /// The CreateEngineer function prompts the user to enter information (ID, name, email, level, and cost) for a new Engineer entity.
    /// It then creates a new Engineer object with the provided information and calls the appropriate data access layer method to create
    /// the entity in the data source.
    /// If successful, it displays a success message; otherwise, it displays an error message if an exception occurs during the creation process.
    /// </summary>
    private static void CreateEngineer()
    {
        try
        {
            Console.Write("id: ");
            int id = int.Parse(Console.ReadLine()!);
            Console.Write("name: ");
            string name = Console.ReadLine()!;
            Console.Write("email: ");
            string email = Console.ReadLine()!;
            Console.Write("leval: ");
            EngineerExperience level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
            Console.Write("cost: ");
            double cost = double.Parse(Console.ReadLine()!);

            Engineer newEngineer = new(id, name, email, level, cost);

            s_dalEngineer!.Create(newEngineer);

            Console.WriteLine("Entity created successfully.");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); };

    }

    /// <summary>
    /// The CreateTask function prompts the user to enter various information (description, alias, isMilestone,
    /// complexityLevel, createdAtDate, scheduledDate, startDate, forecastDate, deadlineDate, completeDate, deliverable, remarks, engineerId) for a new Task entity.
    /// It reads the user input and parses it into the appropriate data types. It then creates a new Task object with the provided information and calls the appropriate data access
    /// layer method to create the entity in the data source.
    /// If successful, it displays a success message indicating that the entity has been created.
    /// </summary>
    private static void CreateTask()
    {
        Console.Write("description: ");
        string description = Console.ReadLine()!;
        Console.Write("alias: ");
        string alias = Console.ReadLine()!;
        Console.Write("isMilestone: ");
        bool isMilestone = bool.Parse(Console.ReadLine()!);
        Console.Write("copmlexityLevel: ");
        EngineerExperience copmlexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
        Console.Write("createdAtDate: ");
        DateTime createdAtDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("scheduledDate: ");
        DateTime scheduledDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("startDate: ");
        DateTime startDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("foresastDate: ");
        TimeSpan foresastDate = TimeSpan.Parse(Console.ReadLine()!);
        Console.Write("deadLineDate: ");
        DateTime deadLineDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("completeDate: ");
        DateTime completeDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("deliverable: ");
        string deliverable = Console.ReadLine()!;
        Console.Write("remarks: ");
        string remarks = Console.ReadLine()!;
        Console.Write("engineerld: ");
        int? engineerld = int.Parse(Console.ReadLine()!);

        DO.Task newTask = new(0, description, alias, isMilestone, copmlexityLevel, createdAtDate, scheduledDate,
            startDate, foresastDate, deadLineDate, completeDate, deliverable, remarks, engineerld);

        s_dalTask!.Create(newTask);

        Console.WriteLine("Entity created successfully.");
    }

    /// <summary>
    /// The CreateDependency function prompts the user to enter the IDs of two tasks, dependentTask and previousTask, to create a dependency between them.
    /// It reads the user input and parses it into integers. It then creates a new Dependency object with the provided task IDs and calls the appropriate data access
    /// layer method to create the dependency in the data source.
    /// If successful, it displays a success message indicating that the entity has been created.
    /// </summary>
    private static void CreateDependency()
    {
        Console.Write("dependentTask: ");
        int dependentTask = int.Parse(Console.ReadLine()!);
        Console.Write("previousTask: ");
        int previousTask = int.Parse(Console.ReadLine()!);

        Dependency newDependency = new(0, dependentTask, previousTask);

        s_dalDependency!.Create(newDependency);

        Console.WriteLine("Entity created successfully.");
    }

    #endregion

    #region Read

    /// <summary>
    /// this funtion snippet defines a method called Read that reads and displays a specific entity based on its ID.
    /// It prompts the user to enter the ID of the desired entity and then uses a switch statement to determine the entity type.
    /// Depending on the entity type (Engineer, Task, or Dependency), it calls the corresponding read method from the appropriate 
    /// DataAccess Layer class and displays the returned entity
    /// </summary>
    /// <param name="entity"></param>
    private static void Read(string entity)
    {
        Console.WriteLine("---- Read by ID ----");
        Console.Write($"Enter the ID of the {entity} you want to display: ");
        int id = int.Parse(Console.ReadLine()!);
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

    #endregion

    #region ReadAll

    /// <summary>
    /// this function  snippet defines a method called ReadAll that reads and displays all entities of a specific type.
    /// It uses a switch statement to determine the entity type and then calls the corresponding ReadAll method from 
    /// the appropriate DataAccess Layer class. It iterates over each returned entity and displays it using Console.WriteLine(). 
    /// The function handles three entity types: Engineer, Task, and Dependency.
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

    #endregion

    #region Update

    /// <summary>
    /// update entity
    /// Passes to the appropriate reference update function
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

    /// <summary>
    ///this function snippet allows the user to update the details of an engineer entity by providing a new name,
    /// email, level, and cost. It validates and retains the existing values if the user provides empty input. 
    ///After collecting the updated details,
    /// it creates a new Engineer object and calls the Update method to update the engineer entity in the data source.
    /// </summary>
    /// <exception cref="Exception"></exception>
    private static void UpdateEngineer()
    {
        Console.Write($"Enter the ID of the engineer you want to update: ");
        int id = int.Parse(Console.ReadLine()!);
        if (s_dalEngineer!.Read(id) is null)
            throw new Exception($"Engineer with ID={id} does Not exist");
        Console.WriteLine(s_dalEngineer!.Read(id));
        string name;
        Console.Write("name: ");
        string? name_input = Console.ReadLine();
        if (name_input == "") { name = s_dalEngineer!.Read(id)!.Name; }
        else { name = name_input!; }
        string email;
        Console.Write("email: ");
        string? email_input = Console.ReadLine();
        if (email_input == "") { email = s_dalEngineer!.Read(id)!.Email; }
        else { email = email_input!; }
        EngineerExperience level;
        Console.Write("leval: ");
        string? level_input = Console.ReadLine();
        if (level_input == "") { level = s_dalEngineer!.Read(id)!.Level; }
        else { level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), level_input!); };
        double cost;
        Console.Write("cost: ");
        string? cost_input = Console.ReadLine();
        if (cost_input == "") { cost = s_dalEngineer!.Read(id)!.Cost; }
        else { cost = double.Parse(cost_input!); }
        Engineer newEngineer = new(id, name!, email!, level, cost);
        s_dalEngineer!.Update(newEngineer);
    }

    /// <summary>
    /// this function snippet allows the user to update the details of an task entity by providing
    /// and retains the existing values if the user provides empty input. 
    /// After collecting the updated details,
    /// it creates a new Engineer object and calls the Update method to update the engineer entity in the data source.
    /// </summary>
    /// <exception cref="Exception"></exception>
    private static void UpdateTask()
    {
        Console.Write($"Enter the ID of the engineer you want to update: ");
        int id = int.Parse(Console.ReadLine()!);

        if (s_dalEngineer!.Read(id) is null)
            throw new Exception($"Task with ID={id} does Not exist");
        Console.WriteLine(s_dalEngineer!.Read(id));

        string description;
        Console.Write("description: ");
        string? description_input = Console.ReadLine();
        if (description_input == "") { description = s_dalTask!.Read(id)!.Description; }
        else { description = description_input!; }

        string alias;
        Console.Write("alias: ");
        string? alias_input = Console.ReadLine();
        if (alias_input == "") { alias = s_dalTask!.Read(id)!.Alias; }
        else { alias = alias_input!; }

        bool isMilestone;
        Console.Write("isMilestone: ");
        string? isMilestone_input = Console.ReadLine();
        if (isMilestone_input == "") { isMilestone = s_dalTask!.Read(id)!.IsMilestone; }
        else { isMilestone = bool.Parse(isMilestone_input!); }

        EngineerExperience copmlexityLevel;
        Console.Write("copmlexityLevel: ");
        string? copmlexityLevel_input = Console.ReadLine();
        if (copmlexityLevel_input == "") { copmlexityLevel = (s_dalTask!.Read(id)!.CopmlexityLevel); }
        else { copmlexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), copmlexityLevel_input!); };

        DateTime createdAtDate;
        Console.Write("createdAtDate: ");
        string? createdAtDate_input = Console.ReadLine();
        if (createdAtDate_input == "") { createdAtDate = s_dalTask!.Read(id)!.CreatedAtDate; }
        else { createdAtDate = DateTime.Parse(createdAtDate_input!); }

        DateTime? scheduledDate;
        Console.Write("scheduledDate: ");
        string? scheduledDate_input = Console.ReadLine();
        if (scheduledDate_input == "") { scheduledDate = s_dalTask!.Read(id)!.ScheduledDate; }
        else { scheduledDate = DateTime.Parse(scheduledDate_input!); }

        DateTime? startDate;
        Console.Write("startDate: ");
        string? startDate_input = Console.ReadLine();
        if (startDate_input == "") { startDate = s_dalTask!.Read(id)!.StartDate; }
        else { startDate = s_dalTask!.Read(id)!.StartDate; }

        TimeSpan? foresastDate;
        Console.Write("foresastDate: ");
        string? foresastDate_input = (Console.ReadLine()!);
        if (foresastDate_input == "") { foresastDate = s_dalTask!.Read(id)!.ForesastDate; }
        else { foresastDate = TimeSpan.Parse(foresastDate_input); }

        DateTime? deadLineDate;
        Console.Write("deadLineDate: ");
        string? deadLineDate_input = (Console.ReadLine()!);
        if (deadLineDate_input == "") { deadLineDate = s_dalTask!.Read(id)!.DeadLineDate; }
        else { deadLineDate = DateTime.Parse(deadLineDate_input); }

        DateTime? completeDate;
        Console.Write("completeDate: ");
        string? completeDate_input = (Console.ReadLine()!);
        if (completeDate_input == "") { completeDate = s_dalTask!.Read(id)!.CompleteDate; }
        else { completeDate = DateTime.Parse(completeDate_input); }

        Console.Write("deliverable: ");
        string? deliverable = Console.ReadLine();
        if (deliverable == "") { deliverable = s_dalTask!.Read(id)!.Deliverable; }

        Console.Write("remarks: ");
        string? remarks = Console.ReadLine();
        if (remarks == "") { remarks = s_dalTask!.Read(id)!.Deliverable; }

        int? engineerld;
        Console.Write("engineerld: ");
        string? engineerld_input = Console.ReadLine();
        if (engineerld_input == "") { engineerld = s_dalTask!.Read(id)!.Engineerld; }
        else { engineerld = int.Parse(engineerld_input!); }

        DO.Task newTask = new(id, description, alias,
            isMilestone, copmlexityLevel, createdAtDate, startDate,
            scheduledDate, foresastDate, deadLineDate,
            completeDate, deliverable, remarks, engineerld);

        s_dalTask!.Update(newTask);
    }

    /// <summary>
    /// this function snippet allows the user to update the details of a dependency entity by providing new values
    /// for the dependentTask and `previousTask
    /// </summary>
    /// <exception cref="Exception"></exception>
    private static void UpdateDependency()
    {
        Console.Write($"Enter the ID of the dependency you want to update: ");
        int id = int.Parse(Console.ReadLine()!);

        Console.WriteLine(s_dalDependency!.Read(id));
        if (s_dalEngineer!.Read(id) is null)
            throw new Exception($"Dependency with ID={id} does Not exist");

        int dependentTask;
        Console.Write("dependentTask: ");
        string? dependentTask_input = Console.ReadLine();
        if (dependentTask_input == "") { dependentTask = s_dalDependency!.Read(id)!.DependentTask; }
        else { dependentTask = int.Parse(dependentTask_input!); }

        int previousTask;
        Console.Write("previousTask: ");
        string? previousTask_input = (Console.ReadLine()!);
        if (previousTask_input == "") { previousTask = s_dalDependency!.Read(id)!.PreviousTask; }
        else { previousTask = int.Parse(previousTask_input); }

        Dependency newDependency = new(id, dependentTask, previousTask);

        s_dalDependency!.Update(newDependency);
    }

    #endregion

    #region Delete

    /// <summary>
    /// The Delete function is responsible for deleting an entity (Engineer, Task, or Dependency) 
    /// based on the entity type specified as a parameter. It prompts the user to enter the ID of the entity
    /// they want to delete, and then calls the appropriate data access layer method to delete the entity from the data source. 
    /// It displays a success message if the deletion is successful, or an error message if an exception occurs during the deletion process.
    /// </summary>
    /// <param name="entity"></param>
    private static void Delete(string entity)
    {
        try
        {
            Console.WriteLine($"---- Delete {entity}  ----");
            Console.Write($"Enter the ID of the {entity} you want to delete: ");
            int id = int.Parse(Console.ReadLine()!);

            switch (entity)
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

    #endregion
}
