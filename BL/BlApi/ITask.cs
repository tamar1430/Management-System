namespace BlApi;

public interface ITask
{
    public IEnumerable<BO.Task> getTaskrsList(Func<BO.Task, bool>? filter = null);
    public BO.Task TaskDetailsRequest(int id);
    public void addTask(BO.Task engineer);
    public void deleteTask(int id);
    public void updateTaskData(BO.Task engineer);
}
