using System.Reflection;
using Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;

public partial class AudioAppDbContext : DbContext, IAudioAppDbContext
{
    private readonly string _connectionString;

    public AudioAppDbContext(IOptions<AppSettings> settings)
    {
        _connectionString = settings.Value.DatabaseConnectionString;
    }
    
    public AudioAppDbContext(DbContextOptions<AudioAppDbContext> options, IOptions<AppSettings> settings)
        : base(options)
    {
        _connectionString = settings.Value.DatabaseConnectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
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