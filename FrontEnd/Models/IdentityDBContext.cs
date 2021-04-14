using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Models
{
    public partial class IdentityDBContext : IdentityDbContext
    {
        public IdentityDBContext()
        {
        }

        public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:proyectofinalpropuestas.database.windows.net,1433;Database=PrograAvanzadaWeb;User Id=sqlFidelitas;Password=Fidelitas123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);// we have to do this because we are inheriting from IdentityDbContext<ApplicationUser> not DbContext

           

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
