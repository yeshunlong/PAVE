using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace WorkflowManager.Core.Models
{
    public class Task
    {
        public Task()
        {

        }
        public Task(Task task)
        {
            this.TaskId = task.TaskId;
            this.Title = task.Title;
            this.Description = task.Description;
            this.Deadline = task.Deadline;
            this.Urgency = task.Urgency;
            this.Type = task.Type;
            this.Status = task.Status;
            this.AssignedTo = task.AssignedTo;
        }

        public int TaskId { get; set; }  // 主键
        public string Title { get; set; }  // 任务名称
        public string Description { get; set; }  // 任务描述
        public DateTime Deadline { get; set; }  // 截止日期
        public int Urgency { get; set; }  // 紧急度（1=普通，2=紧急）
        public TaskType Type { get; set; }  // 任务类型（枚举）
        public TaskStatus Status { get; set; }  // 任务状态（枚举）
        // 使用 AssignedTo 作为外键
        public int? AssignedTo { get; set; }  // 分配给员工的ID，可能为空
        public DateTime CreatedAt { get; set; }  // 创建时间
        // 导航属性，表示与该任务相关联的员工
        public Employee Employee { get; set; }
        // 剩余时间
        [NotMapped]
        public int RemainingDays
        {
            get
            {
                int days = (int)(Deadline - DateTime.Now).TotalDays;
                if (days < 0)
                {
                    return 0;
                }
                if (days > 8)
                {
                    return 8;
                }
                return days;
            }
        }
        // 当前步骤需要时间
        [NotMapped]
        public string TimeLeft
        {
            get
            {
                if (Status == TaskStatus.Pending)
                {
                    return "0:30";
                }
                if (Status == TaskStatus.InProgress)
                {
                    return "2:00";
                }
                if (Status == TaskStatus.Review)
                {
                    return "1:00";
                }
                if (Status == TaskStatus.Testing)
                {
                    return "1:30";
                }
                return "0:20";
            }
        }
        // 状态图标
        [NotMapped]
        public string StatusIcon
        {
            get
            {
                string status = "Doing";
                if (Status == TaskStatus.Pending) status = "Pending";
                if (AssignedTo == null) status = "UnAssigned";
                return $"/Images/{status}.png";
            }
        }
        // 需要能力
        [NotMapped]
        public string Capability
        {
            get
            {
                if (Type == TaskType.Normal)
                {
                    return "Require ARD Capability";
                }
                if (Type == TaskType.Paid)
                {
                    return "Require DAR Capability";
                }
                if (Type == TaskType.Defect)
                {
                    return "Require S3C Capability";
                }
                return "Require Basic Capability";
            }
        }
        // 任务类型颜色
        [NotMapped]
        public string TypeColor
        {
            get
            {
                if (Type == TaskType.Normal)
                {
                    return "White";
                }
                if (Type == TaskType.Paid)
                {
                    return "LightYellow";
                }
                if (Type == TaskType.Defect)
                {
                    return "LightPink";
                }
                return "LightGray";
            }
        }

        public void CopyTaskInfo(Task selectedTask)
        {
            // 赋值所有属性
            if (selectedTask == null) return;
            //this.TaskId = selectedTask.TaskId;
            this.Title = selectedTask.Title;
            this.Description = selectedTask.Description;
            this.Deadline = selectedTask.Deadline;
            this.Status = selectedTask.Status;
            this.Urgency = selectedTask.Urgency;
            this.Type = selectedTask.Type;
            this.AssignedTo = selectedTask.AssignedTo;
        }
    }
    // 任务类型枚举
    public enum TaskType
    {
        Normal,  // 普通需求
        Paid,     // 付费需求
        Defect    // 缺陷
    }

    // 任务状态枚举
    public enum TaskStatus
    {
        Pending,  // 等待中
        InProgress,     // 实现中
        Review,         // Review中
        Testing     // 单元测试中
    }
}
