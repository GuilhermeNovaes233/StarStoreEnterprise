using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Star.Catalog.API.Models;
using Star.Core.Data;
using Star.Core.Messages;
using System.Linq;
using System.Threading.Tasks;

namespace Star.Catalog.API.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set;  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            //Caso algum campo de entidade não seja mapeado configuraremos para ter valor máximo de varchar(100)
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}