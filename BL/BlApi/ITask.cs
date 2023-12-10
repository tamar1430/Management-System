namespace BlApi;

public interface ITask
{
    public IEnumerable<BO.Task> GetTaskrsList(Func<BO.Task, bool>? filter = null);
    public BO.Task TaskDetailsRequest(int id);
    public void AddTask(BO.Task engineer);
    public void DeleteTask(int id);
    public void UpdateTaskData(BO.Task engineer);
}
