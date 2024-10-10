using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkflowManager.Core.Interfaces;
using WorkflowManager.Core.Models;

namespace WorkflowManager.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseContext _context;

        public EmployeeRepository(DatabaseContext context)
        {
            _context = context;
        }

        // 获取所有员工
        public List<Employee> GetAllEmployees()
        {
            // 查询Tasks表中AssignedTo字段与Employees表中EmployeeId字段相等的记录
            List<Employee> employees = _context.Employees.Include(e => e.Tasks).ToList();
            return employees;
        }

        // 根据ID获取单个员工
        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.Include(e => e.Tasks).FirstOrDefault(e => e.EmployeeId == id);
        }

        // 创建员工
        public void CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        // 更新员工
        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        // 删除员工
        public void DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
    }
}
