namespace DalTest;
using DalApi;
using DO;
using Dal;


public static class Initialization
{
    const int MIN_ID = 100000000;
    const int MAX_ID = 999999999;
    const int MIN_COST = 4000;
    const int MAX_COST = 10000;
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
            EngineerExperience _level = (EngineerExperience)enumValues.GetValue(randomIndex);


            double _cost = s_rand.Next(MIN_COST, MAX_COST) % 100;


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

            bool _isMilestone;

            if (s_rand.Next(1, 10) % 2 == 1)
                _isMilestone = true;
            else
                _isMilestone = false;

            // Get all values of the enum
            Array enumValues = Enum.GetValues(typeof(EngineerExperience));

            // Generate a random index
            int randomIndex = s_rand.Next(enumValues.Length);

            // Get the random enum value
            EngineerExperience _copmlexityLevel = (EngineerExperience)enumValues.GetValue(randomIndex);

            DateTime _createdAtDate = DateTime.Now;
            DateTime _scheduledDate = _createdAtDate.AddDays(s_rand.Next(10, 100));
            DateTime _startDate = _scheduledDate.AddDays(s_rand.Next(-10, 10));
            TimeSpan _foresastDate = TimeSpan.FromDays(s_rand.Next(1, 365));
            DateTime _deadLineDate = _scheduledDate + _foresastDate;
            DateTime _completeDate = _startDate.AddDays(s_rand.Next(0, 500));

            string _deliverable = "deliverable deliverable deliverable deliverable";
            string _remarks = "remarks remarks remarks remarks remarks";
            int _engineerld = RandomIdEngineer.randomIdEngineer();

            Task newTask = new(0,_description, _alias, _isMilestone, _copmlexityLevel,
                _createdAtDate, _scheduledDate, _startDate, _foresastDate, _deadLineDate, _completeDate, _deliverable, _remarks, _engineerld);

            s_dalTask!.Create(newTask);

        }

    }
}

