using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;
using Smart.Utilities;

namespace Smart.Pages.Manage
{
    public class UsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _signInManager;

        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        [BindProperty]
        public Areas.Identity.Pages.Account.RegisterModel.InputModel UserForm { get; set; }

        public UsersModel(ApplicationDbContext context, UserManager<User> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public async Task OnGet()
        {
            Users = await _context.Users
                .Include(u => u.UserRoles).ThenInclude(u => u.Role)
                .Select(u => new UserViewModel
                {
                    UserId = u.Id,
                    Name = u.LastName + ", " + u.FirstName,
                    Roles = string.Join(',', u.UserRoles.Select(r => r.Role.Name)),
                    Email = u.Email
                }).ToListAsync();
        }

        public async Task<IActionResult> OnGetUserFormAsync(int? userId)
        {
            InputModel model = new InputModel();
            model.Roles = await _context.Roles.Select(u => new InputModel.UserRole()
            {
                RoleId = u.Id,
                Name = u.Name
            }).ToListAsync();

            if (userId.HasValue)
            {
                var user = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    return NotFound();
                }

                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.UserId = user.Id;

                foreach (var role in model.Roles)
                {
                    if (user.UserRoles.Any(u => u.RoleId == role.RoleId))
                    {
                        role.IsSelected = true;
                    }
                }
            }

            return new PartialViewResult()
            {
                ViewName = "_UserForm",
                ViewData = new ViewDataDictionary<InputModel>(ViewData, model)
            };

        }

        public async Task<IActionResult> OnPostSaveUserAsync(InputModel model)
        {
            User user;

            // CONDITION: User already exists
            if (model.UserId.HasValue)
            {
                user = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Id == model.UserId.Value);

                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.NormalizedEmail = model.Email.ToUpper();
                user.UserName = model.Email;
                user.NormalizedUserName = model.Email.ToUpper();

                await _context.SaveChangesAsync();

                // Password
                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    var result = await _signInManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                    if (!result.Succeeded)
                    {
                        return BadRequest("Incorrect current password entered.");
                    }
                }
            }
            else
            {
                user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email
                };
                var result = await _signInManager.CreateAsync(user, model.NewPassword);
                if (!result.Succeeded)
                {
                    return BadRequest("User creation failed.");
                }


            }

            // Roles
            foreach (var role in model.Roles)
            {
                bool userIsInRole = await _signInManager.IsInRoleAsync(user, role.Name);
                if (role.IsSelected)
                {
                    if (!userIsInRole)
                    {
                        var result = await _signInManager.AddToRoleAsync(user, role.Name);
                        if (!result.Succeeded)
                        {
                            return BadRequest($"Failed to add user to {role.Name} role.");
                        }
                    }
                }
                else
                {
                    if (userIsInRole)
                    {
                        var result = await _signInManager.RemoveFromRoleAsync(user, role.Name);
                        if (!result.Succeeded)
                        {
                            return BadRequest($"Failed to remove user from {role.Name} role.");
                        }
                    }
                }
            }

            return RedirectToPage();
        }

        public class UserViewModel
        {
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Roles { get; set; }
        }
        public class InputModel
        {
            public int? UserId { get; set; }
            [Required]
            [MaxLength(32)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Required]
            [MaxLength(32)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string CurrentPassword { get; set; }

            [RequiredIfNoValue(new string[] { nameof(UserId) })]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New Password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public List<UserRole> Roles { get; set; }

            public class UserRole
            {
                public int RoleId { get; set; }
                public bool IsSelected { get; set; }
                public string Name { get; set; }
            }
        }

    }
}