namespace DalTest;
using DalApi;
using DO;
using Dal;
using System.Runtime.CompilerServices;

public static class Initialization
{
    const int MIN_ID = 100000000;
    const int MAX_ID = 999999999;
    const int MIN_COST = 100;
    const int MAX_COST = 600;
    private static IEngineer? s_dalEngineer;
    private static ITask? s_dalTask;
    private static IDependency? s_dalDependency;

    private static readonly Random s_rand = new();

    private static void createEngineers()
    {
        string[] engineerNames =
    {
        "Dani Levi", "Eli Amar", "Yair Cohen",
        "Ariela Levin", "Dina Klein", "Shira Israelof"
    };
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
            while (s_dalEngineer!.Read(_id) != null);

            string _name = engineerNames[i];
            string _email = engineerEmails[i];

            // Get all values of the enum
            Array enumValues = Enum.GetValues(typeof(EngineerExperience));

            // Generate a random index
            int randomIndex = s_rand.Next(enumValues.Length);

            // Get the random enum value
            EngineerExperience _level = (EngineerExperience)enumValues.GetValue(randomIndex)!;


            double _cost = s_rand.Next(MIN_COST, MAX_COST);


            Engineer newEngineer = new(_id, _name, _email, _level, _cost);

            s_dalEngineer!.Create(newEngineer);
        }
    }





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

            // Get all values of the enum
            Array enumValues = Enum.GetValues(typeof(EngineerExperience));

            // Generate a random index
            int randomIndex = s_rand.Next(enumValues.Length);

            // Get the random enum value
            EngineerExperience _copmlexityLevel = (EngineerExperience)enumValues.GetValue(randomIndex)!;

            DateTime _createdAtDate = DateTime.Now.AddDays(s_rand.Next(-100, -1)); ;
            DateTime? _scheduledDate = null;
            DateTime? _startDate = null;
            TimeSpan?   _foresastDate = null;
            DateTime? _deadLineDate = null;
            DateTime? _completeDate = null;

            //DateTime _createdAtDate = DateTime.Now.AddDays(s_rand.Next(-100, -1)); ;
            //DateTime _scheduledDate = _createdAtDate.AddDays(s_rand.Next(10, 100));
            //DateTime _startDate = _scheduledDate.AddDays(s_rand.Next(-10, 10));
            //TimeSpan _foresastDate = TimeSpan.FromDays(s_rand.Next(1, 365));
            //DateTime _deadLineDate = _scheduledDate + _foresastDate;
            //DateTime _completeDate = _startDate.AddDays(s_rand.Next(0, 500));

            string _deliverable = "deliverable deliverable deliverable deliverable";
            string _remarks = "remarks remarks remarks remarks remarks";
            int? _engineerld = null;

            Task newTask = new(0,_description, _alias, _isMilestone, _copmlexityLevel,
                _createdAtDate, _scheduledDate, _startDate, _foresastDate, _deadLineDate, _completeDate, _deliverable, _remarks, _engineerld);

            s_dalTask!.Create(newTask);

        }

    }

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

    private static void createDependency(int x, int y)
    {
        Dependency newDependency1 = new(0, x, y);
        s_dalDependency!.Create(newDependency1);
    }

    public static void Do(IEngineer? dalEngineer, ITask? dalTask, IDependency? dalDependency)
    {
        s_dalEngineer = dalEngineer ?? throw new Exception("DAL can not be null!");
        s_dalTask = dalTask ?? throw new Exception("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new Exception("DAL can not be null!");

        createEngineers();
        createTasks();
        createDependencys();
    }

}

