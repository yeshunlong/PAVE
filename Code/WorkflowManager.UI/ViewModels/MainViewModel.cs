using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WorkflowManager.Core.Interfaces;
using WorkflowManager.Core.Models;
using WorkflowManager.Core.Services;

namespace WorkflowManager.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ITaskService _taskService;
        private readonly IEmployeeService _employeeService;

        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<Task> _tasks;

        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Employee> Employees { get; set; }
        public Employee SelectedEmployee { get; set; }
        public ObservableCollection<Task> UnAssignedTasks { get; set; }

        public MainViewModel(ITaskService taskService, IEmployeeService employeeService)
        {
            _taskService = taskService;
            _employeeService = employeeService;

            // 初始化数据
            Tasks = new ObservableCollection<Task>(_taskService.GetAllTasks());
            Employees = new ObservableCollection<Employee>(_employeeService.GetAllEmployees());
            UnAssignedTasks = new ObservableCollection<Task>(_taskService.GetUnAssignedTasks());
        }

        internal void UpdateTasks()
        {
            Tasks.Clear();
            foreach (var task in _taskService.GetAllTasks())
            {
                Tasks.Add(task);
            }
            Employees.Clear();
            foreach (var employee in _employeeService.GetAllEmployees())
            {
                Employees.Add(employee);
            }
            UnAssignedTasks.Clear();
            foreach (var task in _taskService.GetUnAssignedTasks())
            {
                UnAssignedTasks.Add(task);
            }
        }

        internal void ReOrderTask(Task selectedTask, Task replacedTask)
        {
            Tasks.Clear();
            Task selectedTaskCopy = new Task(selectedTask);
            Task replacedTaskCopy = new Task(replacedTask);
            foreach (var task in _taskService.GetAllTasks())
            {
                if (task.TaskId == selectedTaskCopy.TaskId)
                {
                    task.CopyTaskInfo(replacedTaskCopy);
                }
                else if (task.TaskId == replacedTaskCopy.TaskId)
                {
                    task.CopyTaskInfo(selectedTaskCopy);
                }
                Tasks.Add(task);
            }
            Employees.Clear();
            foreach (var employee in _employeeService.GetAllEmployees())
            {
                Employees.Add(employee);
            }
            UnAssignedTasks.Clear();
            foreach (var task in _taskService.GetUnAssignedTasks())
            {
                UnAssignedTasks.Add(task);
            }
        }
    }
}
