using System.Data;
using System.Data.Common;
using System.Reflection;
using Contracts;
using LinqToDB.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Persistence;

public partial class AudioAppDbContext : DbContext, IAudioAppDbContext
{
    public AudioAppDbContext()
    {

    }
    
    public AudioAppDbContext(DbContextOptions<AudioAppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //dynamically load all entity and query type configurations
        var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
            (type.BaseType?.IsGenericType ?? false)
            && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)));

        foreach (var typeConfiguration in typeConfigurations)
        {
            var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration)!;
            configuration.ApplyConfiguration(modelBuilder);
        }

        base.OnModelCreating(modelBuilder);
    }
}
