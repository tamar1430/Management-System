namespace DO;

/// <summary>
/// record class for dependencys
/// </summary>
/// <param name="Id"></param>
/// <param name="DependentTask"></param>
/// <param name="PreviousTask"></param>
public record Dependency
(
	int Id,
    int DependentTask,
    int PreviousTask
)
{
 public Dependency() : this(0, 0, 0) { }//defualt constractor

}
