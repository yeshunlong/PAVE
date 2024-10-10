using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WorkflowManager.Core.Interfaces;
using WorkflowManager.Core.Models;
using WorkflowManager.UI.ViewModels;

namespace WorkflowManager.UI.Views
{
    public partial class TaskDetailView : Window
    {
        private Task selectedTask;
        private readonly ITaskService _taskService;

        public TaskDetailView(Task selectedTask)
        {
            InitializeComponent();
            this.selectedTask = selectedTask;

            var taskId = selectedTask.TaskId;
            _taskService = WorkflowManager.UI.App.ServiceProvider.GetRequiredService<ITaskService>();

            this.DataContext = new TaskDetailViewModel(taskId, _taskService);
        }

        public TaskDetailView(int taskId, ITaskService taskService)
        {
            InitializeComponent();

            // 绑定 ViewModel，传入 taskId 和 taskService
            this.DataContext = new TaskDetailViewModel(taskId, taskService);
        }
    }
}
