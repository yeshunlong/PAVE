using System.Collections.Generic;
using WorkflowManager.Core.Models;

namespace WorkflowManager.Core.Interfaces
{
    public interface ITaskRepository
    {
        List<Task> GetAllTasks();
        Task GetTaskById(int id);
        void CreateTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(int id);
    }
}
