using Microsoft.EntityFrameworkCore;
using WorkflowManager.Core.Models;

public class DatabaseContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // �滻����������ַ���
        optionsBuilder.UseMySQL("server=localhost;database=workflowmanagerdb;user=root;password=a46513;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>()
            .Property(t => t.Type)
            .HasConversion<string>();  // ��ö�ٴ洢Ϊ�ַ���

        modelBuilder.Entity<Task>()
            .Property(t => t.Status)
            .HasConversion<string>();  // ��ö�ٴ洢Ϊ�ַ���

        // ���� Employee �� Task ֮��Ĺ�ϵ
        modelBuilder.Entity<Task>()
            .HasOne(t => t.Employee)  // һ��������һ��Ա��
            .WithMany(e => e.Tasks)   // һ��Ա�������ж������
            .HasForeignKey(t => t.AssignedTo);  // ָ�����
    }
}
