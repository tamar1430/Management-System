
namespace DO;

/// <summary>
/// record class for engineers
/// </summary>
/// <param name="Id">Personal unique ID of the engineer</param>
/// <param name="Name"></param>
/// <param name="Email"></param>
/// <param name="Level"></param>
/// <param name="Cost"></param>
public record Engineer
(
	int Id,
	string Name,
	string Email,
    EngineerExperience Level, 
	double Cost
)
{
	public Engineer():this(0,"","", EngineerExperience.Novice, 0) { }//defualt constractor

    public static explicit operator Engineer(List<Engineer> v)
    {
        throw new NotImplementedException();
    }
}
