using Demo.DataRepo;
using Demo.Interface;
using Demo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItems>> GetAllTasks(string role, int userId)
        {
            if (role == "Admin")
            {
                return await _context.Tasks.ToListAsync();
            }
            return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<TaskItems> GetTaskById(int id, string role, int userId)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return null;

            if (role == "Admin" || task.UserId == userId)
                return task;

            return null;
        }

        public async Task CreateNewTask(TaskItems task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

    }
}
