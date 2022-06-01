using Microsoft.EntityFrameworkCore;

namespace WepsysEmployees.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<UpdateEmployees> UpdateEmployees { get; set; }
    }
}
