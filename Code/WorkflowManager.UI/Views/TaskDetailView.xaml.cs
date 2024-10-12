using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WorkflowManager.Core.Interfaces;
using WorkflowManager.Core.Models;
using WorkflowManager.UI.ViewModels;

namespace WorkflowManager.UI.Views
{
    public partial class TaskDetailView : Window
    {
        private Task selectedTask;
        private readonly ITaskService _taskService;
        private readonly IEmployeeService _employeeService;

        public event Action<string> UpdateMainView;

        public TaskDetailView(Task selectedTask)
        {
            InitializeComponent();
            this.selectedTask = selectedTask;

            var taskId = selectedTask.TaskId;
            _taskService = WorkflowManager.UI.App.ServiceProvider.GetRequiredService<ITaskService>();
            _employeeService = WorkflowManager.UI.App.ServiceProvider.GetRequiredService<IEmployeeService>();

            this.DataContext = new TaskDetailViewModel(taskId, _taskService, _employeeService);

            // 如果TaskDetailViewModel中的EmployeeName为空，那么ComboBoxBorderStatusSelector不可编辑
            if (string.IsNullOrEmpty(((TaskDetailViewModel)this.DataContext).EmployeeName))
            {
                ComboBoxBorderStatusSelector.IsEnabled = false;
            }
        }

        public TaskDetailView(int taskId, ITaskService taskService, IEmployeeService employeeService)
        {
            InitializeComponent();

            // 绑定 ViewModel，传入 taskId 和 taskService
            this.DataContext = new TaskDetailViewModel(taskId, taskService, employeeService);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var storyboard = (Storyboard)FindResource("ButtonMouseEnterStoryboard");
                storyboard.Begin();
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var storyboard = (Storyboard)FindResource("ButtonMouseLeaveStoryboard");
                storyboard.Begin();
            }
        }

        private void ComboBox_MouseEnter(object sender, MouseEventArgs e)
        {
            var border = (Border)((ComboBox)sender).Parent;
            var storyboard = (Storyboard)FindResource("ComboBoxMouseEnterStoryboard");
            storyboard.Begin();
        }

        private void ComboBox_MouseLeave(object sender, MouseEventArgs e)
        {
            var border = (Border)((ComboBox)sender).Parent;
            var storyboard = (Storyboard)FindResource("ComboBoxMouseLeaveStoryboard");
            storyboard.Begin();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var model = this.DataContext as TaskDetailViewModel;
            model.SaveTask();

            UpdateMainView?.Invoke("Update");
            this.Close();
        }
    }
}
