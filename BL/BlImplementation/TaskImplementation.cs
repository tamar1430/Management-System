using BlApi;
namespace BlImplementation;
private DalApi.IDal _dal = DalApi.Factory.Get;



internal class TaskImplementation : ITask
{
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
