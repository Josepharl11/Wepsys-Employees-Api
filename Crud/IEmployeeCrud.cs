using Microsoft.AspNetCore.Mvc;
using WepsysEmployees.Models;

namespace WepsysEmployees.Crud
{
    public interface IEmployeeCrud
    {
        Task<IEnumerable<Employees>> GetEmployees();
        Task<Employees> GetById(Guid empId);
        Task<Employees> PostEmployees(PostTheEmployees postemp);
        Task<Employees> UpdateEmployees(Guid id, UpdateEmployees updateemp);
        Task<Employees> DeleteEmployees(Guid empId);

        Boolean GetEmployeesByEmail(string email);
        Boolean EmployeesExists(string lastName);
    }
}
