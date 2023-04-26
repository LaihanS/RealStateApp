using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealStateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Identity.Contexts
{
    public class IdentityContext: IdentityDbContext<User>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API
            #region TableNames
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Identity");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });
            #endregion

            #region "Primary Keys"

            #endregion

            #region Relaciones


            #endregion

            #region Config

            #region Post
            //modelBuilder.Entity<Publicacion>().Property(post => post.id).IsRequired().HasMaxLength(150);
            #endregion

            #endregion
        }
    }
}