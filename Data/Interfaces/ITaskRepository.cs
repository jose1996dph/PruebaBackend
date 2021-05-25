using Domain.Models;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface ITaskRepository
    {
        Task Create(Task Task);
        void Update(Task Task);
        IEnumerable<Task> All(int userId);
        Task Get(int id);
        void Delete(int id);
    }
}
