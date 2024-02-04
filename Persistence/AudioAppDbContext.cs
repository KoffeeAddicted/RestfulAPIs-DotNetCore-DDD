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
        optionsBuilder.UseNpgsql("User ID=koffee;Password=koffee;Host=ec2-13-212-99-75.ap-southeast-1.compute.amazonaws.com;Port=5432;Database=app_name;Pooling=true;Minimum Pool Size=0;Maximum Pool Size=200;Timeout=60;Application Name=AppName");
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
