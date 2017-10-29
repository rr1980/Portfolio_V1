using Microsoft.AspNetCore.Identity;

namespace RR.Migration
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string role) : base(role)
        {

        }
        //public string Name { get; set; }

        //public string Vorname { get; set; }

        //public string Password { get; set; }
    }
}
