using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Data;
using TaskManager.API.model;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
            => await _context.Users.ToListAsync();

        public async Task<User?> GetByIdAsync(int id)
            => await _context.Users.FindAsync(id);

        public async Task AddAsync(User user)
            => await _context.Users.AddAsync(user);

        public void Delete(User user)
            => _context.Users.Remove(user);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}