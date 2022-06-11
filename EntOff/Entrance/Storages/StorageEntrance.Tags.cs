using EntOff.Api.Models.Entities.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using EntOff.Models.Entities.History;

namespace EntOff.Api.Entrance.Storages
{
    public partial class StorageEntrance
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Histo> History { get; set; }

        public async ValueTask<Tag> InsertTagAsync(Tag tag)
        {
            using var ident = new StorageEntrance(this.configuration );

            EntityEntry<Tag> tagEntityEntry = await ident.Tags.AddAsync(tag);
            await ident.SaveChangesAsync();

            return tagEntityEntry.Entity;
        }
        public async ValueTask<Histo> InsertHistoryAsync(Histo his)
        {
            using var ident = new StorageEntrance(this.configuration);

            EntityEntry<Histo> tagEntityEntry = await ident.History.AddAsync(his);
            await ident.SaveChangesAsync();

            return tagEntityEntry.Entity;
        }

        public IQueryable<Histo> SelectAllHistory() => this.History;
        public IQueryable<Tag> SelectAllTags() =>
            this.Tags;
        

        public async ValueTask<Tag> SelectTagByIdAsync(Guid tagId)
        {
            using var ident = new StorageEntrance(this.configuration);
            ident.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await ident.Tags.FindAsync(tagId);
        }

        public async ValueTask<Tag> SelectTagByUserIdAsync(Guid userId)
        {
            using var ident = new StorageEntrance(this.configuration);
            ident.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await ident.Tags.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async ValueTask<Tag> UpdateTagAsync(Tag tag)
        {
            using var ident = new StorageEntrance(this.configuration);

            EntityEntry<Tag> tagEntityEntry =
                ident.Tags.Update(tag);

            await ident.SaveChangesAsync();

            return tagEntityEntry.Entity;
        }

        private static void ConfigureTagEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>(action =>
            {
                action
                    .Property(prop => prop.Status)
                    .HasConversion(
                        x => x.ToString(),
                        x => (TagStatus)Enum.Parse(typeof(TagStatus), x));
            });
        }
        private static void ConfigureHistoryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Histo>(action =>
            {
                action
                    .Property(prop => prop.InOut);
                    
            });
        }
    }
}
