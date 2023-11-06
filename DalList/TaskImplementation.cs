
namespace Dal;
using DalApi;
using DO;
using System;


public class TaskImplementation : ITask
{
    public int Create(Task task)
    {
        int newId = DataSource.Config.NextTaskId;
        Task newTask = task with { Id = newId };
        DataSource.Tasks.Add(newTask);
        return newId;
    }

    public void Delete(int id)
    {
        Task? task = DataSource.Tasks.Find(task => task.Id == id);
        if (task == null)
            throw new Exception($"Task with ID={id} does Not exist");
        else
            DataSource.Tasks.Remove(task);
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(task => task.Id == id);
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task newTask)
    {
        Task? previousTask = DataSource.Tasks.Find(task => task.Id == newTask.Id);
        if (previousTask == null)
            throw new Exception($"Task with ID={newTask.Id} does Not exist");
        DataSource.Tasks.Remove(previousTask);
        DataSource.Tasks.Add(newTask);
    }
}