using Microsoft.EntityFrameworkCore;

namespace WepsysEmployees.Models
{
    [Keyless]
    public class UpdateEmployees
    {
        public string Email { get; set; }
        public string Position { get; set; }
    }
}
