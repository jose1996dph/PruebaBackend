using Data.Interfaces;
using Domain.Models;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class TaskRepository : ITaskRepository
    {
        public Task Create(Task task)
        {
            using var database = new DataBase();

            database.Add(task);

            database.SaveChanges();

            return task;
        }

        public IEnumerable<Task> All(int userId)
        {
            using var database = new DataBase();

            var query = database.Tasks.AsQueryable();

            if (userId != 0)
            {
                query = query.Where(x => x.UserId == userId);
            }

            return query.ToList();
        }

        public Task Get(int id)
        {
            using var database = new DataBase();

            return database.Tasks.Find(id) ?? throw new NullReferenceException("Task not fount");
        }

        public void Update(Task task)
        {
            using var database = new DataBase();

            var oldTask = database.Tasks.Find(task.TaskId) ?? throw new NullReferenceException("Task not fount");

            oldTask.Title = task.Title;
            oldTask.Priority = task.Priority;
            oldTask.Description = task.Description;
            oldTask.StartDate = task.StartDate;
            oldTask.DueDate = task.DueDate;
            oldTask.UserId = task.UserId;

            database.Update(oldTask);

            database.SaveChanges();
        }
        public void Delete(int id)
        {
            using var database = new DataBase();

            var task = database.Tasks.Find(id) ?? throw new NullReferenceException("Task not fount");

            database.Tasks.Remove(task);

            database.SaveChanges();
        }
    }
}
