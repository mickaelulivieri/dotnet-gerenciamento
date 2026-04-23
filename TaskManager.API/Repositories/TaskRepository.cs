using TaskManager.API.Data;
using TaskManager.API.model;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllAsync()
            => await _context.Tasks.ToListAsync();

        public async Task<TaskItem?> GetByIdAsync(int id)
            => await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id);

        public async Task<List<TaskItem>> GetByUserIdAsync(int userId)
            => await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

        public async Task AddAsync(TaskItem task)
            => await _context.Tasks.AddAsync(task);

        public void Delete(TaskItem task)
            => _context.Tasks.Remove(task);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}