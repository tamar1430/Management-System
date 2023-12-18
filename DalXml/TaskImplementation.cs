
namespace Dal;
using DalApi;
using DO;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// create new task
    /// </summary>
    /// <param name="task"></param>
    /// <returns>id of the new task</returns>
    public int Create(Task task)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        int newId = Config.NextTaskId;
        Task newTask = task with { Id = newId };
        tasks.Add(newTask);
        XMLTools.SaveListToXMLSerializer(tasks, "tasks");
        return newId;
    }

    /// <summary>
    /// delete task
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? task = (from task1 in tasks
                              where task1.Id == id
                              select task1).ToList().FirstOrDefault();
        if (task == null)
            throw new DalDoesNotExistException($"Task with ID={id} does Not exist");
        else
           tasks.Remove(task);
        XMLTools.SaveListToXMLSerializer<Task>(tasks, "tasks");
    }

    /// <summary>
    /// The task with the id received
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task? Read(int id)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return tasks.Where(task => task.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Reads task that meet a certain condition
    /// </summary>
    /// <param name="filter">Pointer to a boolean function, delegate of type Func</param>
    /// <returns></returns>
    public Task? Read(Func<Task, bool> filter)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return tasks.Where(filter).FirstOrDefault();
    }

    /// <summary>
    /// Read all tasks or tasks that meet a certain condition
    /// </summary>
    /// <returns>the task list</returns>
    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (filter == null)
            return tasks.Select(task => task);
        else
            return tasks.Where(filter);
    }

    /// <summary>
    /// update task
    /// </summary>
    /// <param name="newTask"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Task task)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? previousTask = tasks.Where(task1 => task1.Id == task.Id).FirstOrDefault();
        if (previousTask == null)
            throw new DalDoesNotExistException($"Task with ID={task.Id} does Not exist");
        tasks.Remove(previousTask);
        tasks.Add(task);
        XMLTools.SaveListToXMLSerializer(tasks, "tasks");
    }
}
