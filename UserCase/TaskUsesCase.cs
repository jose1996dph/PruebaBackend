using Data.Interfaces;
using Domain.Models;
using Domain.Requests;
using System.Collections.Generic;

namespace UserCase
{
    public class TaskUsesCase
    {
        readonly ITaskRepository taskRepository;
        public TaskUsesCase(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }
        public Task Create(CreateTaskRequest taskRequest)
        {
            return taskRepository.Create(taskRequest.ToTask());
        }

        public void Update(UpdateTaskRequest taskRequest)
        {
            taskRepository.Update(taskRequest.ToTask());
        }

        public IEnumerable<Task> All(int userId)
        {
            return taskRepository.All(userId);
        }

        public Task Get(int id)
        {
            return taskRepository.Get(id);
        }

        public void Delete(int id)
        {
            taskRepository.Delete(id);
        }
    }
}
