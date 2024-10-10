using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowManager.Core.Models
{
    public class Task
    {
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
