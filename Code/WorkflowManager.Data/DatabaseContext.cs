using Microsoft.EntityFrameworkCore;
using WorkflowManager.Core.Models;

public class DatabaseContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 替换成你的连接字符串
        optionsBuilder.UseMySQL("server=localhost;database=workflowmanagerdb;user=root;password=a46513;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>()
            .Property(t => t.Type)
            .HasConversion<string>();  // 将枚举存储为字符串

        modelBuilder.Entity<Task>()
            .Property(t => t.Status)
            .HasConversion<string>();  // 将枚举存储为字符串

        // 配置 Employee 和 Task 之间的关系
        modelBuilder.Entity<Task>()
            .HasOne(t => t.Employee)  // 一个任务有一个员工
            .WithMany(e => e.Tasks)   // 一个员工可以有多个任务
            .HasForeignKey(t => t.AssignedTo);  // 指定外键
    }
}
