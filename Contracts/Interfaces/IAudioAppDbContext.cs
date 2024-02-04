using LinqToDB.Common;
using Microsoft.EntityFrameworkCore;

namespace Contracts;

public partial interface IAudioAppDbContext
{
    #region Methods

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves all changes made in this context to the database
        /// </summary>
        /// <returns>The number of state entries written to the database</returns>
        Int32 SaveChanges();

        Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default);
        #endregion
}