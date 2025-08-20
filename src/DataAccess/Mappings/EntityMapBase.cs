using ChurchBulletin.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ChurchBulletin.DataAccess.Mappings;

public abstract class EntityMapBase<T> : IEntityFrameworkMapping where T : EntityBase<T>, new()
{
    public void Map(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<T>(entity =>
        {
            entity.ToTable(typeof(T).Name, "dbo");
            entity.UsePropertyAccessMode(PropertyAccessMode.Field);
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Id)
                .IsRequired()
                .HasValueGenerator<SequentialGuidValueGenerator>()
                .ValueGeneratedOnAdd()
                .HasDefaultValue(Guid.Empty);
        });
    }
    
    protected abstract void MapMembers(EntityTypeBuilder<T> entity);
}