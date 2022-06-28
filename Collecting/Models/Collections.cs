using Microsoft.AspNetCore.Identity;

namespace Collecting.Models
{
    public class Collections
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public IdentityUser User { get; set; }
        public int UserId { get; set; }
    }
}
