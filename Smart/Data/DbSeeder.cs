using Microsoft.AspNetCore.Identity;
using Smart.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data
{
    public class DbSeeder
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _context;

        public DbSeeder(RoleManager<Role> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public void SeedDatabase()
        {
            if (!_context.Roles.Any())
            {
                var roles = new Role[]
                {
                    new Role { Name = Role.Roles.admin },
                    new Role { Name = Role.Roles.instructor },
                    new Role { Name = Role.Roles.socialWorker }
                };

                foreach (var role in roles)
                {
                    _ = _roleManager.CreateAsync(role).Result;
                }
            }
        }
    }
}
