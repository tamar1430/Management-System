﻿using BlApi;
using DalApi;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace BlImplementation;

internal class EngineerImplementation : BlApi.IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void Create(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0) throw new BO.BlInorrectData("Engineer's id isn't correct");
        if (boEngineer.Name.Length <= 0) throw new BO.BlInorrectData("Engineer's name isn't correct");
        if (boEngineer.Cost <= 0) throw new BO.BlInorrectData("Engineer's cost isn't correct");

        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        bool isValid = Regex.IsMatch(boEngineer.Email, pattern);
        if (!isValid) throw new BO.BlInorrectData("Engineer's email isn't correct");

        DO.Engineer doEngineer = new(boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);

        try
        {
            int idEngineer = _dal.Engineer.Create(doEngineer);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        DO.Task? taskEngineer = _dal!.Task!.Read(t => t.EngineerId == id && t.IsMilestone);
        if (taskEngineer != null)
            throw new BO.BlDeletionImpossible("can't delete engineer that do task now");

        // לבדוק איך לבדוק אם בצע משימה בעבר

        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} doesn't exists", ex);
        }
    }

    public BO.Engineer Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        DO.Task? taskEngineer = _dal!.Task!.Read(t => t.EngineerId == id && t.IsMilestone);

        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = taskEngineer != null ? new BO.TaskInEngineer { Id = taskEngineer.Id, Alias = taskEngineer.Alias } : null
        };

    }

    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll(filter!=null ? (Func<DO.Engineer, bool>)filter :null )
                let taskEngineer = _dal!.Task!.Read(t => t.EngineerId == doEngineer.Id && t.IsMilestone)
                select new BO.Engineer
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level,
                    Cost = doEngineer.Cost,
                    Task = taskEngineer != null ? new BO.TaskInEngineer { Id = taskEngineer.Id, Alias = taskEngineer.Alias } : null
                });
    }

    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0) throw new BO.BlInorrectData("Engineer's id isn't correct");
        if (boEngineer.Name.Length <= 0) throw new BO.BlInorrectData("Engineer's name isn't correct");
        if (boEngineer.Cost <= 0) throw new BO.BlInorrectData("Engineer's cost isn't correct");

        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        bool isValid = Regex.IsMatch(boEngineer.Email, pattern);
        if (!isValid) throw new BO.BlInorrectData("Engineer's email isn't correct");

        DO.Engineer doEngineer = new(boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }
}


