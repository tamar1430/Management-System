﻿
namespace BlTest;
using BO;
using System;

/// <summary>
/// Program
/// </summary>
internal class Program
{
    private static readonly Random s_rand = new();

    static readonly BlApi.Bl s_bl = BlApi.Factory.Get();

    /// <summary>
    /// Main
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        try
        {
            DisplayMainMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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
                Console.WriteLine("4. CreatingProjectSchedule ");
                Console.WriteLine("5. Reset ");
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

                        DisplayMilestoneMenu();

                        break;
                    case "4":
                        DateTime start = DateTime.Now;
                        Console.WriteLine("start project date:");
                        DateTime.TryParse(Console.ReadLine(), out start);
                        DateTime finish = DateTime.Now.AddYears(1);
                        Console.WriteLine("finish project date:");
                        DateTime.TryParse(Console.ReadLine(), out finish);
                        s_bl.Milestone.CreatingProjectSchedule(start, finish);

                        break;
                    case "5":
                        s_bl.SpecialOperations.Reset();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }

        }
    }

    /// <summary>
    /// DisplaySubMenu
    /// </summary>
    /// <param name="entityName"></param>
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
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    /// <summary>
    /// DisplayMilestoneMenu
    /// </summary>
    public static void DisplayMilestoneMenu()
    {
        try
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Console.WriteLine("Select the method you want to execute:");
                Console.WriteLine("1. Exiting the main menu");
                Console.WriteLine("2. Object display by identifier");
                Console.WriteLine("3. Updating existing object data");
                Console.Write("Enter your choice: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        exitMenu = true;
                        break;
                    case "2":
                        readObject("Milestone");
                        break;
                    case "3":
                        updateObject("Milestone");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }

    }

    /// <summary>
    /// createObject
    /// </summary>
    /// <param name="entityName"></param>
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

    /// <summary>
    /// createTask
    /// </summary>
    static void createTask()///A function that receives data from the user for the task and creates a new task
    {
        string description = GetInput("Enter the task description: ");
        string alias = GetInput("Enter the task alias: ");
        DateTime? scheduledStartDate = GetNullDateTimeInput("Enter the task start date and time (YYYY-MM-DD HH:mm:ss): ") ?? DateTime.Now;
        DateTime? deadLine = GetNullDateTimeInput("Enter the task deadline date and time (YYYY-MM-DD HH:mm:ss): ") ?? DateTime.Now.AddYears(1);
        TimeSpan requiredEffortTime = new TimeSpan(GetIntInput("Enter the task requiredEffortTime (in days) : "), 0, 0, 0);
        string deliverables = GetInput("Enter the task deliverables: ");
        string remarks = GetInput("Enter the task Remarks: ");
        Console.WriteLine("Enter the dependencies tasks");
        string? read = Console.ReadLine();
        List<int>? dependenciesId = read != "" && read is not null ? read.Split(" ")?.Select(n => int.Parse(n)).ToList() : null;
        List<BO.TaskInList> dependencies = new();
        if (dependenciesId is not null) foreach (var Id in dependenciesId)
            {
                try
                {
                    BO.Task depTask = s_bl.Task.Read(Id);
                    dependencies.Add(
                        new BO.TaskInList()
                        {
                            Id = Id,
                            Alias = depTask.Alias,
                            Description = depTask.Description,
                            Status = depTask.Status is not null ? (BO.Status)depTask.Status! : Status.Unscheduled,
                        });
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        int? engineerId = GetNullIntInput("Enter the engineers Id ");
        BO.EngineerInTask? engineer = engineerId is not null ? new EngineerInTask()
        {
            Id = (int)engineerId,
            Name = s_bl.Engineer.Read((int)engineerId).Name,
        } : null;
        BO.Task task = new BO.Task
        {
            Id = 1,
            Description = description,
            Alias = alias,
            CreatedAtDate = DateTime.Now,
            Status = 0,
            RequiredEffortTime = requiredEffortTime,
            ScheduledStartDate = scheduledStartDate,
            DeadLine = deadLine,
            Deliverable = deliverables,
            Remarks = remarks,
            Engineer = engineer,
            CopmlexityLevel = engineerId is not null ? s_bl.Engineer.Read((int)engineerId!).Level : null,
            Dependencies = dependencies,
        };
        try
        {
            s_bl!.Task.Create(task);
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }

    /// <summary>
    /// createEngineer
    /// </summary>
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
        catch (Exception e) { Console.WriteLine(e.Message); }
    }

    /// <summary>
    /// readObject
    /// </summary>
    /// <param name="entityName"></param>
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

    /// <summary>
    /// readTask
    /// </summary>
    static void readTask()///Receives from the Task id and if it exists it prints it
    {
        int idTask;
        int.TryParse((GetInput("Enter the Tasks id: ")), out idTask);
        BO.Task? taskRead = s_bl!.Task.Read(idTask);
        Console.WriteLine(taskRead);

    }

    /// <summary>
    /// readEngineer
    /// </summary>
    static void readEngineer()///Receives from the Engineer id and if it exists it prints it
    {
        int idEngineer;
        int.TryParse(GetInput("Enter the Engineer id: "), out idEngineer);
        BO.Engineer? engineerRead = s_bl!.Engineer.Read(idEngineer);
        Console.WriteLine(engineerRead);
    }

    /// <summary>
    /// readAllObjects
    /// </summary>
    /// <param name="entityName"></param>
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

    /// <summary>
    /// readAllTasks
    /// </summary>
    static void readAllTasks()///Prints all entities of Task
    {
        List<BO.Task?> taskList = s_bl!.Task.ReadAll(null).ToList();
        foreach (var task in taskList)
        {
            Console.WriteLine(task);
        }
    }

    /// <summary>
    /// readAllEngineers
    /// </summary>
    static void readAllEngineers()
    {
        List<BO.Engineer?> engineerList = s_bl!.Engineer.ReadAll().ToList();
        foreach (var engineer in engineerList)
        {
            Console.WriteLine(engineer);
        }
    }

    /// <summary>
    /// deleteObject
    /// </summary>
    /// <param name="entityName"></param>
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

    /// <summary>
    /// deleteTask
    /// </summary>
    static void deleteTask()///Gets the id from the Tasks and deletes it if it exists
    {
        try
        {
            int idTask;
            int.TryParse(GetInput("Enter the Tasks id: "), out idTask);
            s_bl!.Task.Delete(idTask);
        }
        catch (Exception e) { Console.WriteLine(e.Message); }

    }

    /// <summary>
    /// deleteEngineer
    /// </summary>
    static void deleteEngineer()///Gets the id from the Engineer and deletes it if it exists
    {
        try
        {
            int idEngineer;
            int.TryParse(GetInput("Enter the Engineer id: "), out idEngineer);
            s_bl!.Engineer.Delete(idEngineer);
        }
        catch (Exception e) { Console.WriteLine(e.Message); }

    }

    /// <summary>
    /// updateObject
    /// </summary>
    /// <param name="entityName"></param>
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

    /// <summary>
    /// updateTask
    /// </summary>
    static void updateTask()
    {
        int idTask;
        int.TryParse(GetInput("Enter the Tasks id: "), out idTask);
        BO.Task? task = s_bl!.Task.Read(idTask);
        if (task != null)
        {
            string? description = GetInput("Enter the task description: ");
            string? alias = GetInput("Enter the task alias: ");
            DateTime? scheduledStartDate = GetNullDateTimeInput("Enter the task start date and time (YYYY-MM-DD HH:mm:ss): ") ?? DateTime.Now;
            DateTime? deadLine = GetNullDateTimeInput("Enter the task deadline date and time (YYYY-MM-DD HH:mm:ss): ") ?? DateTime.Now.AddYears(1);
            int? requiredEffortTimeNumber = GetNullIntInput("Enter the task requiredEffortTime (in days) : ");
            TimeSpan? requiredEffortTime = requiredEffortTimeNumber is not null ? new TimeSpan((int)requiredEffortTimeNumber, 0, 0, 0) : null;
            string? deliverables = GetInput("Enter the task deliverables: ");
            string? remarks = GetInput("Enter the task Remarks: ");
            Console.WriteLine("Enter the dependencies tasks");
            string? read = Console.ReadLine();
            List<int>? dependenciesId = read != "" && read is not null ? read.Split(" ")?.Select(n => int.Parse(n)).ToList() : null;
            List<BO.TaskInList> dependencies = new();
            if (dependenciesId is not null) foreach (var Id in dependenciesId)
                {
                    try
                    {
                        BO.Task depTask = s_bl.Task.Read(Id);
                        dependencies.Add(
                            new BO.TaskInList()
                            {
                                Id = Id,
                                Alias = depTask.Alias,
                                Description = depTask.Description,
                                Status = depTask.Status is not null ? (BO.Status)depTask.Status! : Status.Unscheduled,
                            });
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            int? engineerId = GetNullIntInput("Enter the engineers Id ");
            BO.EngineerInTask? engineer = engineerId is not null ? new EngineerInTask()
            {
                Id = (int)engineerId,
                Name = s_bl.Engineer.Read((int)engineerId).Name,
            } : null;
            BO.Task updatedTask = new BO.Task
            {
                Id = task.Id,
                Description = description != "" ? description : task.Description,
                Alias = alias != "" ? alias : task.Alias,
                Status = GetStatusInput("enter the status "),
                StartDate = task.StartDate,
                ScheduledStartDate = task.ScheduledStartDate,
                ForecastDate = task.ForecastDate,
                CompleteDate = task.CompleteDate,
                DeadLine = task.DeadLine,
                Deliverable = deliverables != "" ? deliverables : task.Deliverable,
                Remarks = remarks != "" ? remarks : task.Remarks,
                Engineer = engineerId is not null?engineer:null,
                CopmlexityLevel = task.CopmlexityLevel,
            };
            s_bl!.Task.Update(updatedTask);

        }

    }

    /// <summary>
    /// updateEngineer
    /// </summary>
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
            string? alias = GetInput("Enter the alias  ");
            TaskInEngineer? taskInEngineer = new TaskInEngineer { Id = taskId ?? 0, Alias = alias };

            BO.Engineer updatedEngineer = new BO.Engineer
            {
                Id = engineer.Id,
                Name = name != "" ? name : engineer.Name,
                Email = email != "" ? email : engineer.Email,
                Level = level ?? engineer.Level,
                Cost = cost ?? engineer.Cost,
                Task = taskId == null ? null : taskInEngineer
            };
            s_bl!.Engineer.Update(updatedEngineer);
        }
    }

    /// <summary>
    /// GetInput
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    static string GetInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? " ";
    }

    /// <summary>
    /// GetNullIntInput
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
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

    /// <summary>
    /// GetIntInput
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// <exception cref="BlNullPropertyException"></exception>
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

    /// <summary>
    /// GetNullDateTimeInput
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static DateTime? GetNullDateTimeInput(string message)
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

    /// <summary>
    /// GetTimeSpanInput
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// <exception cref="BlNullPropertyException"></exception>
    public static TimeSpan GetTimeSpanInput(string message)
    {
        TimeSpan inputMutable;
        string? input = GetInput(message);
        bool success = TimeSpan.TryParse(input, out TimeSpan parsedValue);

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

    /// <summary>
    /// GetNullDoubleInput
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
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

    /// <summary>
    /// GetBoolInput
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    static bool GetBoolInput(string message)
    {
        Console.Write(message);
        string? input = Console.ReadLine();
        return bool.Parse(input!);
    }

    /// <summary>
    /// GetDateTimeInput
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    static DateTime? GetDateTimeInput(string message)
    {
        DateTime date;
        Console.WriteLine(message);
        if (!DateTime.TryParse(Console.ReadLine(), out date))
            return null;
        return date;
    }

    /// <summary>
    /// GetCopmlexityLevelInput
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    static BO.EngineerExperience GetCopmlexityLevelInput(string message)
    {
        Console.Write(message);
        string? level = Console.ReadLine();
        BO.EngineerExperience experienceLevel;
        Enum.TryParse(level, out experienceLevel);
        return experienceLevel;
    }

    /// <summary>
    /// GetStatusInput
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    static BO.Status GetStatusInput(string message)
    {
        Console.Write(message);
        string? status = Console.ReadLine();
        BO.Status statusLevel;
        Enum.TryParse(status, out statusLevel);
        return statusLevel;
    }

    /// <summary>
    /// readMilestone
    /// </summary>
    static void readMilestone()
    {
        int idMilestone;
        int.TryParse((GetInput("Enter the Milestone id: ")), out idMilestone);
        BO.Milestone? milestoneRead = s_bl!.Milestone.Read(idMilestone);
        Console.WriteLine(milestoneRead);

    }

    /// <summary>
    /// updateMilestone
    /// </summary>
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