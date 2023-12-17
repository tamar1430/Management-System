namespace DalTest;
using DalApi;
using DO;


/// <summary>
/// class Initialization
/// </summary>
public static class Initialization
{
    const int MIN_ID = 100000000;
    const int MAX_ID = 999999999;
    const int MIN_COST = 100;
    const int MAX_COST = 600;

    //private static IEngineer? s_dalEngineer;//stage 1
    //private static ITask? s_dalTask;//stage1
    //private static IDependency? s_dalDependency;//stage1
    private static IDal? s_dal; //stage 2

    private static readonly Random s_rand = new();

    /// <summary>
    /// This function appears to be a loop that iterates over an array of engineer names.
    /// Inside the loop, it generates a random ID for each engineer using the s_rand random number generator object. 
    /// It then checks if the generated ID already exists in the data accessed by the s_dalEngineer data access layer object. 
    /// If the ID already exists, it continues generating new IDs until a unique one is found.
    /// After generating the ID, it assigns the current engineer name to the _name variable and the corresponding engineer email to the _email variable.
    /// It then retrieves all values of an enum called EngineerExperience using the Enum.GetValues method and stores them in an array called enumValues.
    /// Next, it generates a random index using the s_rand random number generator object and assigns it to the randomIndex variable.Finally,
    /// it retrieves the engineer experience level at the randomly generated index from the enumValues array and assigns it to the _level variable.
    /// Overall, it seems that this code generates random IDs, assigns engineer names and emails,
    /// and randomly assigns engineer experience levels to each engineer in the array.
    /// </summary>
    private static void createEngineers()
    {
        //a array with names of engineers
        string[] engineerNames =
        {
             "Dani Levi", "Eli Amar", "Yair Cohen",
             "Ariela Levin", "Dina Klein", "Shira Israelof"
        };
        //a array with the engineers emails
        string[] engineerEmails =
        {
            "DaniL@gmail.com", "EliA@gmail.com", "YairC@gmail.com",
            "ArielaL@gmail.com", "DinaK@gmail.com", "ShiraI@gmail.com"
        };

        for (int i = 0; i < engineerNames.Length; i++)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dal!.Engineer!.Read(_id) != null);
            string _name = engineerNames[i];
            string _email = engineerEmails[i];
            Array enumValues = Enum.GetValues(typeof(EngineerExperience));// Get all values of the enum
            int randomIndex = s_rand.Next(enumValues.Length);   // Generate a random index
            EngineerExperience _level = (EngineerExperience)enumValues.GetValue(randomIndex)!; // Get the random enum value
            double _cost = s_rand.Next(MIN_COST, MAX_COST);
            Engineer newEngineer = new(_id, _name, _email, _level, _cost);
            s_dal.Engineer!.Create(newEngineer);
        }
    }

    /// <summary>
    /// this function generates multiple tasks with various properties using predefined descriptions and aliases.
    /// It assigns random complexity levels,
    /// creates random creation dates, and saves the tasks using the DAL
    /// </summary>
    private static void createTasks()
    {
        string[] arrTaskDescription =
             { "Buy groceries","Prepare dinner","Organize the room","Read a book", "Translate an article" ,"Write a blog post", "Follow homework instructions","Meet a friend",
        "Define goals for the week","Exercise for 30 minutes","Complete a puzzle","Learn a new language", "Create a budget", "Write a thank-you note",
        "Plant flowers","Watch a movie","Volunteer for a cause","Write a poem", "Learn to play a musical instrument","Plan a weekend getaway"};

        string[] arrTaskAlias =
             {"Shopping", "Cooking", "Cleaning", "Reading", "Translation", "Blogging", "Homework", "Socializing", "Planning",
       "Fitness","Game", "Language learning", "Financial planning","Appreciation","Gardening","Entertainment","Community service","Creative writing",
       "Music practice",  "Travel planning"};

        for (int i = 0; i < arrTaskDescription.Length; i++)
        {
            string _description = arrTaskDescription[i];
            string _alias = arrTaskAlias[i];
            bool _isMilestone = false;
            Array enumValues = Enum.GetValues(typeof(EngineerExperience));  // Get all values of the enum
            int randomIndex = s_rand.Next(enumValues.Length);  // Generate a random index
            EngineerExperience _copmlexityLevel = (EngineerExperience)enumValues.GetValue(randomIndex)!;// Get the random enum value
            DateTime _createdAtDate = DateTime.Now.AddDays(s_rand.Next(-100, -1));
            DateTime? _scheduledDate = null;
            DateTime? _startDate = null;
            TimeSpan? _foresastDate = null;
            DateTime? _deadLineDate = null;
            DateTime? _completeDate = null;
            string _deliverable = "deliverable deliverable deliverable deliverable";
            string _remarks = "remarks remarks remarks remarks remarks";
            int? _engineerId = null;
            Task newTask = new(0, _description, _alias, _isMilestone, _copmlexityLevel,
                _createdAtDate, _scheduledDate, _startDate, _foresastDate, _deadLineDate, _completeDate, _deliverable, _remarks, _engineerId);
            s_dal!.Task!.Create(newTask);

        }
    }

    /// <summary>
    /// create Dependencys
    /// </summary>
    private static void createDependencys()
    {
        createDependency(5, 1);
        createDependency(5, 2);
        createDependency(5, 3);
        createDependency(7, 1);
        createDependency(7, 2);
        createDependency(7, 3);
        createDependency(9, 4);
        createDependency(10, 3);
        createDependency(11, 8);
        createDependency(12, 7);
        createDependency(13, 8);
        createDependency(15, 4);
        createDependency(15, 5);
        createDependency(15, 9);
        createDependency(16, 2);
        createDependency(16, 7);
        createDependency(16, 10);
        createDependency(17, 1);
        createDependency(17, 2);
        createDependency(17, 3);
        createDependency(17, 4);
        createDependency(17, 5);
        createDependency(17, 6);
        createDependency(17, 7);
        createDependency(17, 8);
        createDependency(17, 9);
        createDependency(17, 10);
        createDependency(17, 11);
        createDependency(17, 12);
        createDependency(17, 13);
        createDependency(17, 14);
        createDependency(17, 15);
        createDependency(17, 16);
        createDependency(18, 3);
        createDependency(18, 2);
        createDependency(18, 7);
        createDependency(18, 15);
        createDependency(19, 3);
        createDependency(19, 8);
        createDependency(19, 3);
        createDependency(20, 1);
    }

    /// <summary>
    /// this function creates a dependency relationship between two tasks
    /// identified by their IDs and saves it using the DAL.
    /// </summary>
    /// <param name="x">DependentTask</param>
    /// <param name="y">PreviousTask</param>
    private static void createDependency(int x, int y)
    {
        Dependency newDependency1 = new(0, x, y);
        s_dal!.Dependency!.Create(newDependency1);
    }

    /// <summary>
    /// Initializing lists
    /// </summary>
    /// <param name="dalEngineer"></param>
    /// <param name="dalTask"></param>
    /// <param name="dalDependency"></param>
    /// <exception cref="Exception"></exception>
    public static void Do(IDal dal) //stage 2

    {
        //s_dalEngineer = dalEngineer ?? throw new Exception("DAL can not be null!"); //stage 1
        //s_dalTask = dalTask ?? throw new Exception("DAL can not be null!"); //stage 1
        //s_dalDependency = dalDependency ?? throw new Exception("DAL can not be null!"); //stage 1
        s_dal = dal ?? throw new Exception("DAL object can not be null!"); //stage 

        createEngineers();
        createTasks();
        createDependencys();
    }

}

