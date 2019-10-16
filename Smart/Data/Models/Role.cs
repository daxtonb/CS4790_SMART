using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    public enum RoleEnum
    {
        [DisplayName("Admin")] Admin = 1,
        [DisplayName("Instructor")] Instructor = 2,
        [DisplayName("Social Worker")] SocialWorker = 3,
        [DisplayName("Sponsor")] Sponsor = 4
    }
}
