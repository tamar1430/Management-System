
namespace Dal;
using DalApi;
using DO;
using System.Data;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// create new task
    /// </summary>
    /// <param name="task"></param>
    /// <returns>id of the new task</returns>
    public int Create(Task task)
    {
        int newId = DataSource.Config.NextTaskId;
        Task newTask = task with { Id = newId };
        DataSource.Tasks.Add(newTask);
        return newId;
    }

    /// <summary>
    /// delete task
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        throw new DalDeletionImpossible("can't delete task");
        //Task? task = DataSource.Tasks.Find(task => task.Id == id);
        //if (task == null)
        //    throw new Exception($"Task with ID={id} does Not exist");
        //else
        //    DataSource.Tasks.Remove(task);
    }

    /// <summary>
    /// The task with the id received
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(task => task.Id == id);
    }

    /// <summary>
    /// read all the tasks
    /// </summary>
    /// <returns>the task list</returns>
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    /// <summary>
    /// update task
    /// </summary>
    /// <param name="newTask"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Task newTask)
    {
        Task? previousTask = DataSource.Tasks.Find(task => task.Id == newTask.Id);
        if (previousTask == null)
            throw new Exception($"Task with ID={newTask.Id} does Not exist");
        DataSource.Tasks.Remove(previousTask);
        DataSource.Tasks.Add(newTask);
    }
}