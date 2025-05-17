using Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Interface
{
    public interface ITaskService
    {
        Task<List<TaskItems>> GetAllTasks(string role, int userId);
        Task<TaskItems> GetTaskById(int id, string role, int userId);
        Task CreateNewTask(TaskItems task);
    }
}
