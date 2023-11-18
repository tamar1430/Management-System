﻿
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
    }

    /// <summary>
    /// The task with the id received
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task? Read(int id)
    {
        return DataSource.Tasks.Where(task=>task.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Reads task that meet a certain condition
    /// </summary>
    /// <param name="filter">Pointer to a boolean function, delegate of type Func</param>
    /// <returns></returns>
    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.Where(filter).FirstOrDefault();
    }

    /// <summary>
    /// Read all tasks or tasks that meet a certain condition
    /// </summary>
    /// <returns>the task list</returns>
    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Tasks.Select(task => task);
        else
            return DataSource.Tasks.Where(filter);
    }

    /// <summary>
    /// update task
    /// </summary>
    /// <param name="newTask"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Task newTask)
    {
        Task? previousTask = DataSource.Tasks.Where(task => task.Id == newTask.Id).FirstOrDefault();
        if (previousTask == null)
            throw new DalDoesNotExistException($"Task with ID={newTask.Id} does Not exist");
        DataSource.Tasks.Remove(previousTask);
        DataSource.Tasks.Add(newTask);
    }
}