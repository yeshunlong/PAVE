using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WorkflowManager.Core.Interfaces;
using WorkflowManager.Core.Services;
using WorkflowManager.Data;
using WorkflowManager.Data.Repositories;
using WorkflowManager.UI.ViewModels;
using WorkflowManager.UI.Views;

namespace WorkflowManager.UI
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // 注册数据库上下文
            services.AddDbContext<DatabaseContext>();

            // 注册仓储层
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // 注册服务层
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            // 注册 ViewModel
            services.AddScoped<MainViewModel>();
            services.AddScoped<TaskDetailViewModel>();
        }
    }
}
