using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WorkflowManager.Core.Interfaces;
using WorkflowManager.Core.Models;

namespace WorkflowManager.UI.ViewModels
{
    public class TaskDetailViewModel : INotifyPropertyChanged
    {
        private readonly ITaskService _taskService;
        private readonly IEmployeeService _employeeService;

        private string _status;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Task Task { get; set; }
        public ObservableCollection<string> StatusOptions { get; set; }
        public ObservableCollection<string> AssignedToOptions { get; set; }
        public string EmployeeName { get; set; }

        public string Status {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(_status));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SaveCommand { get; set; }

        public TaskDetailViewModel(int taskId, ITaskService taskService, IEmployeeService employeeService)
        {
            _taskService = taskService;
            _employeeService = employeeService;

            // 加载任务详情
            LoadTaskDetail(taskId);

            // 状态的选择项
            StatusOptions = new ObservableCollection<string> { "Pending", "InProgress", "Review", "Testing" };
            if (Task != null)
            {
                Status = Task.Status.ToString();
            }

            // 初始化分配的员工
            AssignedToOptions = new ObservableCollection<string>();
            foreach (var employee in _employeeService.GetAllEmployees())
            {
                AssignedToOptions.Add(employee.Name);
                if (Task != null && Task.AssignedTo != null && employee.EmployeeId == Task.AssignedTo)
                {
                    EmployeeName = employee.Name;
                }
            }

            // 保存任务的命令
            SaveCommand = new RelayCommand(SaveTask);
        }

        public void SaveTask()
        {
            // 根据EmployeeName查询EmployeeId
            int employeeId = 0;
            foreach (var employee in _employeeService.GetAllEmployees())
            {
                if (employee.Name == EmployeeName)
                {
                    employeeId = employee.EmployeeId;
                    break;
                }
            }
            _taskService.UpdateTask(Task, Status, employeeId);
        }

        private void LoadTaskDetail(int taskId)
        {
            // 从数据库中加载特定任务
            Task = _taskService.GetTaskById(taskId);
        }
    }
}
