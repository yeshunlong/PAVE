using System.Collections.ObjectModel;
using WorkflowManager.Core.Interfaces;
using WorkflowManager.Core.Models;

namespace WorkflowManager.UI.ViewModels
{
    public class MainViewModel
    {
        private readonly ITaskService _taskService;
        private readonly IEmployeeService _employeeService;

        public ObservableCollection<Task> Tasks { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        public Employee SelectedEmployee { get; set; }

        public MainViewModel(ITaskService taskService, IEmployeeService employeeService)
        {
            _taskService = taskService;
            _employeeService = employeeService;

            // 初始化数据
            Tasks = new ObservableCollection<Task>(_taskService.GetAllTasks());
            Employees = new ObservableCollection<Employee>(_employeeService.GetAllEmployees());
        }
    }
}
