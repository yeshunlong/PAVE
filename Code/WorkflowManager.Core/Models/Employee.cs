using System;
using System.Collections.Generic;

namespace WorkflowManager.Core.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }  // 主键
        public string Name { get; set; }  // 员工姓名
        public string Position { get; set; }  // 职位
        public DateTime CreatedAt { get; set; } // 创建时间
        // 导航属性，表示该员工的任务
        public List<Task> Tasks { get; set; }
    }
}
