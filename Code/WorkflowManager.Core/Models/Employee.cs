using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        // 当前状态
        [NotMapped]
        public string Status
        {
            get
            {
                if (Tasks == null || Tasks.Count == 0)
                {
                    return "Idle";
                }
                int total = 0;
                foreach (var task in Tasks)
                {
                    if (task.Status != TaskStatus.Pending)
                    {
                        total++;
                    }
                }
                if (total == 0) {
                    return "Idle";
                }
                if (total == Tasks.Count)
                {
                    return "Overloaded";
                }
                return "Working";
            }
        }
        // 当前状态颜色
        [NotMapped]
        public string StatusColor
        {
            get
            {
                string status = this.Status;
                if (status == "Idle")
                {
                    return "Ivory";
                }
                if (status == "Overloaded")
                {
                    return "Orange";
                }
                if (status == "Working")
                {
                    return "LightBlue";
                }
                return "LightGray";
            }
        }
        // 头像路径
        [NotMapped]
        public string Image
        {
            get
            {
                return $"/Images/{Name}_transparent.png";
            }
        }
        // 工作负载（百分比）
        [NotMapped]
        public double Workload
        {
            get
            {
                if (Tasks == null || Tasks.Count == 0)
                {
                    return 0;
                }
                int total = 0;
                foreach (var task in Tasks)
                {
                    if (task.Status != TaskStatus.Pending)
                    {
                        total++;
                    }
                }
                return 1.0 * total / Tasks.Count;
            }
        }
    }
}
