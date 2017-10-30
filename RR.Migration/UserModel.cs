using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RR.Migration
{

    public class UserModel
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } 

        public string Vorname { get; set; }

        public string PasswordHash { get; set; }
    }
}
