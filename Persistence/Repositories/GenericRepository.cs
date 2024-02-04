using Contracts;
using Domain.Entities.Base;
using Domain.RepositoryInterfaces;
using LinqToDB.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

    /// <summary>
    /// Represents the Entity Framework repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public partial class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        #region Fields

        private readonly IAudioAppDbContext _dbContext;

        private DbSet<TEntity> _entities;

        #endregion

        #region Ctor

        public GenericRepository(IAudioAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Error message</returns>
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_dbContext is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                });
            }

            try
            {
                _dbContext.SaveChanges();
                return exception.ToString();
            }
            catch (Exception ex)
            {
                //if after the rollback of changes the context is still not saving,
                //return the full text of the exception that occurred when saving
                return ex.ToString();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual TEntity GetById(object id)
        {
            return Entities.Find();
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            if (entity is IAuditEntity auditEntity)
                auditEntity.CreatedDateTime = DateTime.UtcNow;
                
            try
            {
                Entities.Add(entity);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            if (entity is IAuditEntity auditEntity)
                auditEntity.CreatedDateTime = DateTime.UtcNow;

            try
            {
                await Entities.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }

            return entity;
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                foreach (var entity in entities)
                {
                    if (entity is IAuditEntity auditEntity)
                    {
                        auditEntity.CreatedDateTime = DateTime.UtcNow;
                    }
                }
                
                Entities.AddRange(entities);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            if (entity is IAuditEntity auditEntity)
                auditEntity.UpdatedTime = DateTime.UtcNow;

            try
            {
                Entities.Update(entity);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            if (entity is IAuditEntity auditEntity)
                auditEntity.UpdatedTime = DateTime.UtcNow;

            try
            {
                Entities.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                foreach (var entity in entities)
                {
                    if (entity is IAuditEntity auditEntity)
                    {
                        auditEntity.UpdatedTime = DateTime.UtcNow;
                    }
                }
                Entities.UpdateRange(entities);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                if (entity is IDeleteEntity deleteEntity)
                {
                    deleteEntity.IsDeleted = true;
                    Entities.Update(entity);
                }
                else
                {
                    Entities.Remove(entity);
                }
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            var entitiesToUpdate = new List<TEntity>();
            var entitiesToRemove = new List<TEntity>();

            foreach (var entity in entities)
            {
                if (entity is IDeleteEntity deleteEntity)
                {
                    deleteEntity.IsDeleted = true;
                    entitiesToUpdate.Add(entity);
                }
                else
                {
                    entitiesToRemove.Add(entity);
                }
            }

            try
            {
                if (entitiesToUpdate.Any())
                {
                    Entities.UpdateRange(entitiesToUpdate);
                }

                if (entitiesToRemove.Any())
                {
                    Entities.RemoveRange(entitiesToRemove);
                }

                _dbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                // Ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _dbContext.Set<TEntity>();
                
                return _entities;
            }
        }

        #endregion
    }