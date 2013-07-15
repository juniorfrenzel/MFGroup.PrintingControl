using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using MFGroup.PrintingControl.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MFGroup.PrintingControl.Repository
{
    public class PrintingControlContext : DbContext
    {
        public DbSet<Material> Materiais { get; set; }
        public DbSet<Marca> Marcas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
