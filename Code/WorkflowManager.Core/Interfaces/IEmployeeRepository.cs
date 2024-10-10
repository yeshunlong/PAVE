using System.Collections.Generic;
using WorkflowManager.Core.Models;

namespace WorkflowManager.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        public void CreateEmployee(Employee employee);
        public void UpdateEmployee(Employee employee);
        public void DeleteEmployee(int id);
    }
}
