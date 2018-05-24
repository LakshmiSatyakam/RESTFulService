using Microsoft.EntityFrameworkCore;
using RESTFulService.Models.Members.DTO;
using System;
using System.Linq;

namespace RESTFulService.Entity
{
    /// <summary>
    /// Members context
    /// </summary>
    public class MembersContext : DbContext
    {
        #region Public properties
        public DbSet<Member> Members { get; set; }
        #endregion

        #region Construction
        public MembersContext(DbContextOptions options) : base(options)
        {
        }
        #endregion

        #region Protected methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Member>()
                .ToTable("Members");

            modelBuilder.Entity<Member>()
                .Property(s => s.Id)
                .IsRequired();

            modelBuilder.Entity<Member>()
                .Property(s => s.FirstName)
                .HasDefaultValue("test");

            modelBuilder.Entity<Member>()
                .Property(s => s.LastName)
                .HasDefaultValue("testLast");

            modelBuilder.Entity<Member>()
                .Property(s => s.DateOfBirth)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Member>()
                .Property(s => s.Email)
                .HasDefaultValue("test@gmail.com");
        } 
        #endregion
    }
}
