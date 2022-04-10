using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Village22.Models
{
    public class VillageContext : IdentityDbContext<User>
    {
        public VillageContext(DbContextOptions<VillageContext> options) : base(options) { }
        public DbSet<TaRequest> TaRequests { get; set; }
        public DbSet<TaRequestStatus> TaRequestStatuses { get; set; }
        public DbSet<TaMatch> TaMatches { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<TaMatchStatus> TaMatchStatuses { get; set; }
        public DbSet<TaContract> TaContracts { get; set; }

        public DbSet<TeachingAssignment> TeachingAssignments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            TaRequestStatus[] taRequestStatuses =
            {
                new TaRequestStatus
                {
                    Id = 1,
                    Name = "Requested"
                },
                new TaRequestStatus
                {
                    Id = 2,
                    Name = "Matching"
                },
                new TaRequestStatus
                {
                    Id = 3, 
                    Name = "Satisfied"
                },
                new TaRequestStatus
                {
                    Id = 4,
                    Name = "Cancelled"
                }
            };
            modelBuilder.Entity<TaRequestStatus>().HasData(taRequestStatuses);

            TaMatchStatus[] taMatchStatuses =
            {
                new TaMatchStatus
                {
                    Id = 1,
                    Name = "In Progress"
                },
                new TaMatchStatus
                {
                    Id = 2,
                    Name = "Approved by Teacher"
                },
                new TaMatchStatus
                {
                    Id = 3,
                    Name = "Approved by Student"
                },
                new TaMatchStatus
                {
                    Id = 4,
                    Name = "Successful"
                },
                new TaMatchStatus
                {
                    Id = 5,
                    Name = "Rejected by Student"
                },
                new TaMatchStatus
                {
                    Id = 6,
                    Name = "Rejected by Teacher"
                }
            };
            modelBuilder.Entity<TaMatchStatus>().HasData(taMatchStatuses);
        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "VillageTeam@1";
            string roleName = "Admin";

            //if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            //if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

        //// this method is written based on `CreateAdminUser`. Can be flawful. //delete this comment when correctness is ensured. 
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            String[] roleNames = { "Teacher", "Ta" };

            foreach (String roleName in roleNames)
            {
                //if role doesn't exist, create it
                if (await roleManager.FindByNameAsync(roleName) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }   
        }
    }
}
