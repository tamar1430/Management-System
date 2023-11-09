namespace DO;


public record Dependency
(
	int Id,//the Dependency's id
    int DependentTask,//The number of the dependent task
    int PreviousTask//The number of the previous task
)
{
 public Dependency() : this(0, 0, 0) { }//defualt constractor

}
