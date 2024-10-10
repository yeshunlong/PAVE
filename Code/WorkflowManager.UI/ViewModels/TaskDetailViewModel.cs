using System.Collections.ObjectModel;
using System.Windows.Input;
using WorkflowManager.Core.Interfaces;
using WorkflowManager.Core.Models;

namespace WorkflowManager.UI.ViewModels
{
    public class TaskDetailViewModel
    {
        private readonly ITaskService _taskService;
        public Task Task { get; set; }
        public ObservableCollection<string> StatusOptions { get; set; }

        public ICommand SaveCommand { get; set; }

        public TaskDetailViewModel(int taskId, ITaskService taskService)
        {
            _taskService = taskService;

            // 假设这是状态的选择项
            StatusOptions = new ObservableCollection<string> { "调查中", "实现中", "Review中", "单元测试中" };

            // 保存任务的命令
            SaveCommand = new RelayCommand(SaveTask);

            // 加载任务详情
            LoadTaskDetail(taskId);
        }

        private void SaveTask()
        {
            _taskService.UpdateTask(Task);
        }

        private void LoadTaskDetail(int taskId)
        {
            // 从数据库中加载特定任务
            Task = _taskService.GetTaskById(taskId);
        }
    }
}
