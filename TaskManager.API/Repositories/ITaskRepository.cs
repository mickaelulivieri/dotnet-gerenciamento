using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.model;

namespace TaskManager.API.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllAsync();

        Task<TaskItem?> GetByIdAsync(int id);

        Task<List<TaskItem>> GetByUserIdAsync(int userId);

        Task AddAsync(TaskItem task);

        void Delete(TaskItem task);
        
        Task SaveAsync();
    }
}