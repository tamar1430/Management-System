using BlApi;
namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void AddTask(BO.Task engineer)
    {
        throw new NotImplementedException();
    }

    public void DeleteTask(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task> GetTaskrsList(Func<BO.Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public BO.Task TaskDetailsRequest(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateTaskData(BO.Task engineer)
    {
        throw new NotImplementedException();
    }
}
