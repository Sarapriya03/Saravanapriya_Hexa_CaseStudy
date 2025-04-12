using System.Collections.Generic;
using PayXpert.entity;

namespace PayXpert.dao
{
    public interface IEmployeeService
    {
        Employee GetEmployeeById(int employeeId);
        List<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void RemoveEmployee(int employeeId);
    }
}
