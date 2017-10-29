using Microsoft.AspNetCore.Identity;

namespace RR.Migration
{

    public class ApplicationUser : IdentityUser
    {
        //public ApplicationUser(string userName) : base(userName)
        //{

        //}
        public string Name { get; set; } 

        public string Vorname { get; set; }

        public string Password { get; set; }
    }
}
