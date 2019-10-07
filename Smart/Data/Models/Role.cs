using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public static class Roles
        {
            public static string admin = "Admin";
            public static string instructor = "Instructor";
            public static string socialWorker = "SocialWorker";
        }
    }
}
