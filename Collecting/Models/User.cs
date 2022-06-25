using Microsoft.AspNetCore.Identity;

namespace Collecting.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}