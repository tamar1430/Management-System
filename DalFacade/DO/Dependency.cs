namespace Do;

public record Dependency
{
	int Id;
	int DependentTask;
	int PreviousTask;
}
