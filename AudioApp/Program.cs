using Contracts;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Repositories;
using Quartz;
using Services;
using Services.Absractions;
using Services.Jobs;
using Services.Middlewares;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

String envInUsed = configuration.GetSection("ASPNETCORE_ENVIRONMENT").Value ?? throw new InvalidOperationException("Can not get ASPNETCORE_ENVIRONMENT value");

WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    EnvironmentName = envInUsed
});

//Config to use Appsetting objects in appsettings json
IConfigurationSection appConfigSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appConfigSection);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<ExceptionHandlingMiddleware>();


builder.Services.AddScoped<IAudioAppDbContext, AudioAppDbContext>(provider =>
{
    var options = provider.GetRequiredService<IOptions<AppSettings>>();
    var dbContextOptions = new DbContextOptionsBuilder<AudioAppDbContext>()
        .UseNpgsql(options.Value.DatabaseConnectionString)
        .Options;
    return new AudioAppDbContext(dbContextOptions, options);
});
builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


// builder.Services.AddQuartz(q =>  
// {
//     // Create a "key" for the job
//     var jobKey = new JobKey("ConvertYoutubeAndUploadS3Job");
//
//     // Register the job with the DI container
//     q.AddJob<ConvertYoutubeAndUploadS3Job>(opts => opts.WithIdentity(jobKey));
//                 
//     // Create a trigger for the job
//     q.AddTrigger(opts => opts
//         .ForJob(jobKey)
//         .WithIdentity("ConvertYoutubeAndUploadS3Job-trigger")
//         .WithCronSchedule("0/50 * * * * ?"));
//
// });
// builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();
app.UseSignatureValidationMiddleware();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();