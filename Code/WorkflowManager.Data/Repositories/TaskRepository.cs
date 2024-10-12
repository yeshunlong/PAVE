using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkflowManager.Core.Interfaces;
using WorkflowManager.Core.Models;

namespace WorkflowManager.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DatabaseContext _context;

        public TaskRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<Task> GetAllTasks()
        {
            return _context.Tasks.Include(t => t.Employee).ToList();
        }

        public Task GetTaskById(int id)
        {
            return _context.Tasks.Include(t => t.Employee).FirstOrDefault(t => t.TaskId == id);
        }

        public void CreateTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Task> GetUnAssignedTasks()
        {
            List<Task> tasks = _context.Tasks.Include(t => t.Employee).ToList();
            return tasks.Where(t => t.Employee == null);
        }

        public void UpdateTask(Task task, string status, int employeeId)
        {
            // 更新任务状态
            task.Status = Enum.Parse<TaskStatus>(status);
            // 更新任务负责人
            task.AssignedTo = employeeId;
            // 更新导航属性
            task.Employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }
    }
}
