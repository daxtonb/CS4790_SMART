using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        [MaxLength(32)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(32)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(128)]
        public override string Email { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ApplicantRating> ApplicantRatings { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Error> Errors { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
