using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Domain.Common;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) { }

        public DbSet<Mejora> Mejoras { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Propiedades> Propiedades { get; set; }
        public DbSet<VentaType> VentaTypes { get; set; }
        public DbSet<PropertyImages> Imagenes { get; set; }

        public DbSet<PropiedadMejora> PropiedadMejoras { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellation = new CancellationToken())
        {
            foreach (var item in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.created = DateTime.Now;
                        item.Entity.createdBy = "Laihusmanguplus";
                        break;

                    case EntityState.Modified:
                        item.Entity.modifiedAt = DateTime.Now;
                        item.Entity.modifiedBy = "Laihusmanguplus";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellation);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Nombres(tablas)
            modelBuilder.Entity<Mejora>().ToTable("Mejoras");
            modelBuilder.Entity<PropertyType>().ToTable("PropertyTypes");
            modelBuilder.Entity<Propiedades>().ToTable("Propiedades");
            modelBuilder.Entity<VentaType>().ToTable("VentaTypes");
            modelBuilder.Entity<PropertyImages>().ToTable("Imagenes");
            modelBuilder.Entity<PropiedadMejora>().ToTable("PropiedadMejoras");
            #endregion

            #region "Primary Keys"
            modelBuilder.Entity<Mejora>().HasKey(entity => entity.id);
            modelBuilder.Entity<PropertyType>().HasKey(entity => entity.id);
            modelBuilder.Entity<Propiedades>().HasKey(entity => entity.id);
            modelBuilder.Entity<VentaType>().HasKey(entity => entity.id);
            modelBuilder.Entity<PropertyImages>().HasKey(entity => entity.id);
            modelBuilder.Entity<PropiedadMejora>().HasKey(entity => entity.id);
            #endregion

            #region Relaciones

            modelBuilder.Entity<Mejora>().HasMany<PropiedadMejora>(prop => prop.PropiedadMejoras).
            WithOne(mejora => mejora.Mejora).
            HasForeignKey(mejora => mejora.MejoraId).
            OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Propiedades>().HasMany<PropiedadMejora>(prop => prop.PropiedadMejoras).
            WithOne(mejora => mejora.Propiedad).
            HasForeignKey(mejora => mejora.PropiedadId).
            OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PropertyType>().HasMany<Propiedades>(proptype => proptype.Propiedades).
            WithOne(propiedad => propiedad.PropiedadType).
            HasForeignKey(propiedad => propiedad.PropertyTypeId).
            OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Propiedades>().HasMany<PropertyImages>(proptype => proptype.Imagenes).
            WithOne(images => images.Propiedad).
            HasForeignKey(images => images.IdPropiedad).
            OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VentaType>().HasMany<Propiedades>(proptype => proptype.Propiedades).
            WithOne(propiedad => propiedad.VentaType).
            HasForeignKey(propiedad => propiedad.VentaTypeId).
            OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Config 

            #region Post
            //modelBuilder.Entity<Publicacion>().Property(post => post.id).IsRequired().HasMaxLength(150);
            #endregion

            #endregion
        }

    }
}
