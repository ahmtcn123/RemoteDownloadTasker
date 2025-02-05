using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Core.Abstracts;
using Core.Domain.Entities;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        //public DbSet<DownloadedFile> DownloadedFiles { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries()
                                    .Where(i => i.State == EntityState.Added)
                                    .Select(i => (GenericFields)i.Entity);

            var modifiedEntities = ChangeTracker.Entries()
                                    .Where(i => i.State == EntityState.Modified)
                                    .Select(i => (GenericFields)i.Entity);

            PrepareEntities(addedEntities);

            //if (modifiedEntities.Count() >= 1)
            //{
            //    UpdateModifiedEntities(modifiedEntities);
            //}

        }

        private void PrepareEntities(IEnumerable<GenericFields> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.CreatedAt == DateTime.MinValue)
                    entity.CreatedAt = DateTime.UtcNow;

            }
        }

        //private void UpdateModifiedEntities(IEnumerable<GenericFields> entities)
        //{
        //    foreach (var entity in entities)
        //    {
        //        if (entity.UpdatedDate == DateTime.MinValue)
        //            entity.UpdatedDate = DateTime.UtcNow;
        //    }
        //}
    }
}
