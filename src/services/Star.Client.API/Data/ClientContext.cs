using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Star.Client.API.Models;
using Star.Core.Data;
using Star.Core.DomainObjects;
using Star.Core.Mediator;
using Star.Core.Messages;
using System.Linq;
using System.Threading.Tasks;

namespace Star.Client.API.Data
{
    public class ClientContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientContext(DbContextOptions<ClientContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;

            if (success) await _mediatorHandler.PublishEvents(this);

            return success;
        }
    }

    public static class MediatorExtensions
    {
        public static async Task PublishEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                    .Entries<Entity>()
                    .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                    .SelectMany(x => x.Entity.Notifications)
                    .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvent());

            var tasks = domainEvents
                .Select(async (domainEvents) =>
                {
                    await mediator.PublishEvent(domainEvents);
                });

            await Task.WhenAll(tasks);
        }
    }
}