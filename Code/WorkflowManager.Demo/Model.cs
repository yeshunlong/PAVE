using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowManager.Demo
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ZIndex { get; set; }
        public int Offset
        {
            get
            {
                return ZIndex * 10;
            }
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }
    }

}
