using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.IdentityModels;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Marks> Marks { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TSC> TSCs { get; set; }
        public DbSet<Notes> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "ApplicationUsers");
            });

            builder.Entity<Subject>().HasData(
                new Subject { Id = Guid.NewGuid(), Title = "Mathematics", IsActive = true },
                new Subject { Id = Guid.NewGuid(), Title = "History", IsActive = true },
                new Subject { Id = Guid.NewGuid(), Title = "Geography", IsActive = true },
                new Subject { Id = Guid.NewGuid(), Title = "English", IsActive = true },
                new Subject { Id = Guid.NewGuid(), Title = "Arabic", IsActive = true },
                new Subject { Id = Guid.NewGuid(), Title = "Sport", IsActive = true },
                new Subject { Id = Guid.NewGuid(), Title = "Science", IsActive = true },
                new Subject { Id = Guid.NewGuid(), Title = "Physics", IsActive = true },
                new Subject { Id = Guid.NewGuid(), Title = "Chemistry", IsActive = true },
                new Subject { Id = Guid.NewGuid(), Title = "Religion", IsActive = true }
                );

        }
    }

}
