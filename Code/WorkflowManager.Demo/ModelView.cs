using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowManager.Demo
{
    public class MainViewModel
    {
        public ObservableCollection<Employee> Employees { get; set; }

        public MainViewModel()
        {
            // 示例数据
            Employees = new ObservableCollection<Employee>
        {
            new Employee { Id = 0, Name = "Alice", Tasks = new List<Task>
                {
                    new Task { Id = 0, Description = "Task A1" , ZIndex = 0},
                    new Task { Id = 1, Description = "Task A2" , ZIndex = 1}
                }},
            new Employee { Id = 1, Name = "Bob", Tasks = new List<Task>
                {
                    new Task { Id = 0, Description = "Task B1" , ZIndex = 0},
                    new Task { Id = 1, Description = "Task B2" , ZIndex = 1},
                    new Task { Id = 1, Description = "Task B3" , ZIndex = 2},
                    new Task { Id = 2, Description = "Task B4" , ZIndex = 3}
                }},
            new Employee { Id = 2, Name = "Charlie", Tasks = new List<Task>
                {
                    new Task { Id = 0, Description = "Task C1" , ZIndex = 0},
                    new Task { Id = 3, Description = "Task C2" , ZIndex = 1}
                }},
        };
        }
    }

}
