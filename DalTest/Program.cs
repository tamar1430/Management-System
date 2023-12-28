﻿using Dal;
using DalApi;
using DO;

namespace DalTest;

internal class Program
{
    static readonly IDal s_dal = Factory.Get; //stage 4

    /// <summary>
    /// this function initializes the program by calling the Do method of the Initialization class and then displays the options menu to the user. 
    /// It also handles any exceptions that might occur during program execution by catching them and displaying the exception message.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        try
        {
            s_dal.SpecialOperations.Reset();
            for (int i = 0; i < 5; i++)
            {
                string _description = "a";
                string _alias = "a";
                bool _isMilestone = false;
                Array enumValues = Enum.GetValues(typeof(EngineerExperience));  // Get all values of the enum
                int randomIndex = i;  // Generate a random index
                EngineerExperience? _copmlexityLevel = (EngineerExperience)enumValues.GetValue(randomIndex)!;// Get the random enum value
                DateTime _createdAtDate = DateTime.Now.AddDays(i);
                DateTime? _scheduledDate = null;
                DateTime? _startDate = null;
                TimeSpan? _requiredEffortTime = null;
                DateTime? _foresastDate = null;
                DateTime? _deadLineDate = null;
                DateTime? _completeDate = null;
                string _deliverable = "deliverable deliverable deliverable deliverable";
                string _remarks = "remarks remarks remarks remarks remarks";
                int? _engineerId = null;
                DO.Task newTask = new(0, _description, _alias, _isMilestone, _createdAtDate, _copmlexityLevel,
                     _scheduledDate, _startDate, _requiredEffortTime, _foresastDate, _deadLineDate, _completeDate, _deliverable, _remarks, _engineerId);
                s_dal!.Task!.Create(newTask);

            }
            s_dal.Dependency.Create(new DO.Dependency(0, 3, 1));
            s_dal.Dependency.Create(new DO.Dependency(0, 3, 2));
            s_dal.Dependency.Create(new DO.Dependency(0, 4, 3));
            s_dal.Dependency.Create(new DO.Dependency(0, 5, 3));
            //Initialization.Do(s_dal);
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

            s_dal.Engineer!.Create(newEngineer);

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
        DateTime foresastDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("requiredEffortTime: ");
        TimeSpan requiredEffortTime = TimeSpan.Parse(Console.ReadLine()!);
        Console.Write("deadLineDate: ");
        DateTime deadLineDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("completeDate: ");
        DateTime completeDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("deliverable: ");
        string deliverable = Console.ReadLine()!;
        Console.Write("remarks: ");
        string remarks = Console.ReadLine()!;
        Console.Write("engineerId: ");
        int? engineerId = int.Parse(Console.ReadLine()!);

        DO.Task newTask = new(0, description, alias, isMilestone,  createdAtDate, copmlexityLevel, scheduledDate,
            startDate,requiredEffortTime, foresastDate, deadLineDate, completeDate, deliverable, remarks, engineerId);

        s_dal.Task!.Create(newTask);

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

        s_dal!.Dependency!.Create(newDependency);

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
                Console.WriteLine(s_dal.Engineer!.Read(id));
                break;
            case "Task":
                Console.WriteLine(s_dal.Task!.Read(id));
                break;
            case "Dependency":
                Console.WriteLine(s_dal.Dependency!.Read(id));
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
                foreach (Engineer? engineer in s_dal!.Engineer!.ReadAll())
                {
                    Console.WriteLine(engineer);
                }
                break;
            case "Task":
                foreach (DO.Task? task in s_dal!.Task!.ReadAll())
                {
                    Console.WriteLine(task);
                }
                break;
            case "Dependency":
                foreach (Dependency? dependency in s_dal!.Dependency!.ReadAll())
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
        if (s_dal!.Engineer!.Read(id) is null)
            throw new Exception($"Engineer with ID={id} does Not exist");
        Console.WriteLine(s_dal!.Engineer!.Read(id));
        string name;
        Console.Write("name: ");
        string? name_input = Console.ReadLine();
        if (name_input == "") { name = s_dal!.Engineer!.Read(id)!.Name; }
        else { name = name_input!; }
        string email;
        Console.Write("email: ");
        string? email_input = Console.ReadLine();
        if (email_input == "") { email = s_dal!.Engineer!.Read(id)!.Email; }
        else { email = email_input!; }
        EngineerExperience level;
        Console.Write("leval: ");
        string? level_input = Console.ReadLine();
        if (level_input == "") { level = s_dal!.Engineer!.Read(id)!.Level; }
        else { level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), level_input!); };
        double cost;
        Console.Write("cost: ");
        string? cost_input = Console.ReadLine();
        if (cost_input == "") { cost = s_dal!.Engineer!.Read(id)!.Cost; }
        else { cost = double.Parse(cost_input!); }
        Engineer newEngineer = new(id, name!, email!, level, cost);
        s_dal!.Engineer!.Update(newEngineer);
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
        Console.Write($"Enter the ID of the task you want to update: ");
        int id = int.Parse(Console.ReadLine()!);

        if (s_dal!.Task!.Read(id) is null)
            throw new Exception($"Task with ID={id} does Not exist");
        Console.WriteLine(s_dal!.Task!.Read(id));

        string description;
        Console.Write("description: ");
        string? description_input = Console.ReadLine();
        if (description_input == "") { description = s_dal!.Task!.Read(id)!.Description; }
        else { description = description_input!; }

        string alias;
        Console.Write("alias: ");
        string? alias_input = Console.ReadLine();
        if (alias_input == "") { alias = s_dal!.Task!.Read(id)!.Alias; }
        else { alias = alias_input!; }

        bool isMilestone;
        Console.Write("isMilestone: ");
        string? isMilestone_input = Console.ReadLine();
        if (isMilestone_input == "") { isMilestone = s_dal!.Task!.Read(id)!.IsMilestone; }
        else { isMilestone = bool.Parse(isMilestone_input!); }

        EngineerExperience? copmlexityLevel;
        Console.Write("copmlexityLevel: ");
        string? copmlexityLevel_input = Console.ReadLine();
        if (copmlexityLevel_input == "") { copmlexityLevel = (s_dal!.Task!.Read(id)!.CopmlexityLevel); }
        else { copmlexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), copmlexityLevel_input!); };

        DateTime createdAtDate;
        Console.Write("createdAtDate: ");
        string? createdAtDate_input = Console.ReadLine();
        if (createdAtDate_input == "") { createdAtDate = s_dal!.Task!.Read(id)!.CreatedAtDate; }
        else { createdAtDate = DateTime.Parse(createdAtDate_input!); }

        DateTime? scheduledDate;
        Console.Write("scheduledDate: ");
        string? scheduledDate_input = Console.ReadLine();
        if (scheduledDate_input == "") { scheduledDate = s_dal!.Task!.Read(id)!.ScheduledDate; }
        else { scheduledDate = DateTime.Parse(scheduledDate_input!); }

        DateTime? startDate;
        Console.Write("startDate: ");
        string? startDate_input = Console.ReadLine();
        if (startDate_input == "") { startDate = s_dal!.Task!.Read(id)!.StartDate; }
        else { startDate = s_dal!.Task!.Read(id)!.StartDate; }

        DateTime? foresastDate;
        Console.Write("foresastDate: ");
        string? foresastDate_input = (Console.ReadLine()!);
        if (foresastDate_input == "") { foresastDate = s_dal!.Task!.Read(id)!.ForesastDate; }
        else { foresastDate = DateTime.Parse(foresastDate_input); }


        TimeSpan? requiredEffortTime;
        Console.Write("requiredEffortTime: ");
        string? requiredEffortTime_input = (Console.ReadLine()!);
        if (requiredEffortTime_input == "") { requiredEffortTime = s_dal!.Task!.Read(id)!.RequiredEffortTime; }
        else { requiredEffortTime = TimeSpan.Parse(requiredEffortTime_input); }

        DateTime? deadLineDate;
        Console.Write("deadLineDate: ");
        string? deadLineDate_input = (Console.ReadLine()!);
        if (deadLineDate_input == "") { deadLineDate = s_dal!.Task!.Read(id)!.DeadLineDate; }
        else { deadLineDate = DateTime.Parse(deadLineDate_input); }

        DateTime? completeDate;
        Console.Write("completeDate: ");
        string? completeDate_input = (Console.ReadLine()!);
        if (completeDate_input == "") { completeDate = s_dal!.Task!.Read(id)!.CompleteDate; }
        else { completeDate = DateTime.Parse(completeDate_input); }

        Console.Write("deliverable: ");
        string? deliverable = Console.ReadLine();
        if (deliverable == "") { deliverable = s_dal!.Task!.Read(id)!.Deliverable; }

        Console.Write("remarks: ");
        string? remarks = Console.ReadLine();
        if (remarks == "") { remarks = s_dal!.Task!.Read(id)!.Deliverable; }

        int? engineerId;
        Console.Write("engineerId: ");
        string? engineerId_input = Console.ReadLine();
        if (engineerId_input == "") { engineerId = s_dal!.Task!.Read(id)!.EngineerId; }
        else { engineerId = int.Parse(engineerId_input!); }

        DO.Task newTask = new(id, description, alias,
            isMilestone,  createdAtDate, copmlexityLevel, scheduledDate,
            startDate, requiredEffortTime, foresastDate, deadLineDate,
            completeDate, deliverable, remarks, engineerId);

        s_dal!.Task!.Update(newTask);
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

        if (s_dal!.Dependency!.Read(id) is null)
            throw new Exception($"Dependency with ID={id} does Not exist");
        Console.WriteLine(s_dal!.Dependency!.Read(id));

        int dependentTask;
        Console.Write("dependentTask: ");
        string? dependentTask_input = Console.ReadLine();
        if (dependentTask_input == "") { dependentTask = s_dal!.Dependency!.Read(id)!.DependentTask; }
        else { dependentTask = int.Parse(dependentTask_input!); }

        int previousTask;
        Console.Write("previousTask: ");
        string? previousTask_input = (Console.ReadLine()!);
        if (previousTask_input == "") { previousTask = s_dal!.Dependency!.Read(id)!.PreviousTask; }
        else { previousTask = int.Parse(previousTask_input); }

        Dependency newDependency = new(id, dependentTask, previousTask);

        s_dal!.Dependency!.Update(newDependency);
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
                    s_dal!.Engineer!.Delete(id);
                    break;
                case "Task":
                    s_dal!.Task!.Delete(id);
                    break;
                case "Dependency":
                    s_dal!.Dependency!.Delete(id);
                    break;

            }


            Console.WriteLine("Entity deleted successfully.");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); };
    }

    #endregion
}
