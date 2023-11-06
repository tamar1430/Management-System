namespace DalTest;
using DalApi;
using DO;


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

            double _cost = s_rand.Next(MIN_COST, MAX_COST) % 100;


            Engineer newEngineer = new(_id, _name, _email, EngineerExperience.Novice, _cost);

            s_dalEngineer!.Create(newEngineer);
        }
    }
}
