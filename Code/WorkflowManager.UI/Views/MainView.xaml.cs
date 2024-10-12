using System.Windows;
using WorkflowManager.UI.ViewModels;
using WorkflowManager.Core.Interfaces; // 引用接口所在的命名空间
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using WorkflowManager.UI.UserControls;

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
    }
}
