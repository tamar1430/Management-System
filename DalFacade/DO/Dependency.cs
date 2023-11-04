namespace DO;

public record Dependency
(
	int Id;
	int DependentTask;
	int PreviousTask;
)
{
 public Dependency() : this(0, 0, 0) { }

}
