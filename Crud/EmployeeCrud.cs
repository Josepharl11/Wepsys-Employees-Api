using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepsysEmployees.Models;

namespace WepsysEmployees.Crud
{
    public class EmployeeCrud : IEmployeeCrud
    {
        private readonly EmployeeContext context;

        public EmployeeCrud(EmployeeContext context)
        {
            this.context = context;
        }

        // GET EMPLOYEES
        public async Task<IEnumerable<Employees>> GetEmployees()
        {
            return await context.Employees.ToListAsync();
        }

        // GET EMPLOYEES BY ID
        public async Task<Employees> GetById(Guid empId)
        {
            return await context.Employees
                .FindAsync(empId);
        }

        // POST EMPLOYEES
        public async Task<Employees> PostEmployees(PostTheEmployees postemp)
        { 
            Employees emp = new Employees { FirstName = postemp.FirstName, LastName = postemp.LastName, Gender = postemp.Gender, DateOfBirth = postemp.DateOfBirth, Email = postemp.Email, Position = postemp.Position, EmployeeId = new Guid() };

            var result = await context.Employees.AddAsync(emp);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        // UPDATE EMPLOYEES
        public async Task<Employees> UpdateEmployees(Guid id, UpdateEmployees updateemp)
        {
            var result = await context.Employees.FindAsync(id);

            if (result != null)
            {
                result.Email = updateemp.Email;
                result.Position = updateemp.Position;

                await context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        // DELETE EMPLOYEES BY ID
        public async Task<Employees> DeleteEmployees(Guid empId)
        {
            var result = await context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == empId);
            if (result != null)
            {
                context.Employees.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        // VALIDATIONS
        // Validation of Employees using their emails.
        public bool GetEmployeesByEmail(string email)
        {
            return context.Employees.Any(emp => emp.Email == email);
        }

        // Validation of Employees using their lastnames.
        public bool EmployeesExists(string lastName)
        {
            return context.Employees.Any(emp => emp.LastName == lastName);
        }
    }
}