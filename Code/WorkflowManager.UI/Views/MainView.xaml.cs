using System.Windows;
using WorkflowManager.UI.ViewModels;
using WorkflowManager.Core.Interfaces; // 引用接口所在的命名空间
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using WorkflowManager.UI.UserControls;
using System.Windows.Documents;
using System.Linq;
using System.Windows.Data;
using WorkflowManager.Core.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace WorkflowManager.UI.Views
{
    public partial class MainView : Window
    {
        private readonly ITaskService _taskService;
        private readonly IEmployeeService _employeeService;

        public MainView()
        {
            InitializeComponent();

            // 从服务提供者获取服务
            _taskService = WorkflowManager.UI.App.ServiceProvider.GetRequiredService<ITaskService>();
            _employeeService = WorkflowManager.UI.App.ServiceProvider.GetRequiredService<IEmployeeService>();

            this.DataContext = new MainViewModel(_taskService, _employeeService);
        }

        private void TaskCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var taskCard = sender as TaskCard;
            if (taskCard != null)
            {
                var selectedTask = taskCard.Task;

                if (selectedTask != null)
                {
                    TaskDetailView detailView = new TaskDetailView(selectedTask);
                    detailView.UpdateMainView += (status) =>
                    {
                        // 更新主视图
                        var viewModel = this.DataContext as MainViewModel;
                        viewModel?.UpdateTasks();
                    };
                    detailView.ShowDialog(); // 以模态对话框形式打开
                }
            }
        }

        private void TaskCard_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var taskCard = sender as TaskCard;
            var currentTask = taskCard?.Task;
            if (currentTask?.Employee == null) return;

            var viewModel = this.DataContext as MainViewModel;
            var matchingTasks = viewModel.Tasks
                .Where(task => task.Employee?.EmployeeId == currentTask.Employee.EmployeeId &&
                               task.RemainingDays == currentTask.RemainingDays &&
                               task.TaskId != currentTask.TaskId)
                .ToList();

            if (matchingTasks.Any())
            {
                var task = matchingTasks.First();
                viewModel?.ReOrderTask(currentTask, task);
            }
        }
    }
}
