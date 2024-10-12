using System.Collections.Generic;
using WorkflowManager.Core.Interfaces;
using WorkflowManager.Core.Models;

namespace WorkflowManager.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public List<Task> GetAllTasks()
        {
            return _taskRepository.GetAllTasks();
        }

        public Task GetTaskById(int id)
        {
            return _taskRepository.GetTaskById(id);
        }

        public void CreateTask(Task task)
        {
            _taskRepository.CreateTask(task);
        }

        public void UpdateTask(Task task)
        {
            _taskRepository.UpdateTask(task);
        }

        public void DeleteTask(int id)
        {
            _taskRepository.DeleteTask(id);
        }

        public IEnumerable<Task> GetUnAssignedTasks()
        {
            return _taskRepository.GetUnAssignedTasks();
        }

        public void UpdateTask(Task task, string status, int employeeId)
        {
            _taskRepository.UpdateTask(task, status, employeeId);
        }
    }
}
