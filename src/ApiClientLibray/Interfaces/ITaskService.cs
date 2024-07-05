using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientLibray.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskModel>> GetTasksAsync();
        Task<TaskModel> CreateTaskAsync(TaskModel task);
        Task UpdateTaskAsync(TaskModel task);
        Task DeleteTaskAsync(int taskId);
    }
}
