using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Models
{
    public class ApplicationdbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationdbContext(DbContextOptions<ApplicationdbContext> options) : base(options)
        {

        }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOptions> QuestionOptions { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<CustomerFeedBack> CustomerFeedBacks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)ProjectsV13;Initial Catalog=foodbookdb;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
