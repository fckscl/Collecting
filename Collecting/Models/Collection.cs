using Microsoft.AspNetCore.Identity;

namespace Collecting.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        //public IdentityUser? User { get; set; }
        public string UserId { get; set; }
    }
}
