using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Repositories.BaseRepository
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region props

        protected readonly ApplicationDbContext _context;
        private DbSet<TEntity> _entitiesDbSet { get; set; }
        public readonly IHttpContextAccessor _httpContextAccessor;

        #endregion props

        #region ctor

        public BaseRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        ~BaseRepository()
        {
        }

        #endregion ctor

        #region public

        public IQueryable<TEntity> GetNoTrackingEntities(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Entities.AsNoTracking();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public IQueryable<TEntity> GetNoTrackingEntitiesIdentityResolution(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Entities.AsNoTrackingWithIdentityResolution();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public IQueryable<TEntity> GetQueryableTable(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Entities.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public virtual Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetNoTrackingEntities();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            var entities = query.ToListAsync();
            return entities;
        }

        public virtual Task<TEntity> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Entities.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            var entity = query.SingleOrDefaultAsync(x => id.Equals(x.Id));
            return entity;
        }

        public virtual Task<TEntity> GetByIdNoTrackingAsync(TKey id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetNoTrackingEntities();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            var entity = query.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return entity;
        }

        public virtual async Task<int> CreateAsync(TEntity entity)
        {
            ValidateAndThrow(entity);
            var currentUserName = GetUserNameInHttpContext();
            entity.SetDefaultValue(currentUserName);
            await Entities.AddAsync(entity);
            var countAffect = _context.SaveChanges();
            return countAffect;
        }

        public virtual async Task<int> CreateAsync(List<TEntity> entities)
        {
            ValidateAndThrow(entities);
            var currentUserName = GetUserNameInHttpContext();
            entities.ForEach(e =>
            {
                e.SetDefaultValue(currentUserName);
            });

            await Entities.AddRangeAsync(entities);
            var countAffect = _context.SaveChanges();
            return countAffect;
        }

        public virtual Task<int> UpdateAsync(TEntity entity)
        {
            ValidateAndThrow(entity);
            var currentUserName = GetUserNameInHttpContext();
            var entry = _context.Entry(entity);
            entity.SetValueUpdate(currentUserName);
            if (entry.State < EntityState.Added)
            {
                entry.State = EntityState.Modified;
            }
            var countAffect = _context.SaveChanges();
            return Task.FromResult(countAffect);
        }

        public virtual Task<int> UpdateAsync(IEnumerable<TEntity> entities)
        {
            var currentUserName = GetUserNameInHttpContext();
            entities.ToList().ForEach(e =>
            {
                ValidateAndThrow(e);
                e.SetValueUpdate(currentUserName);
            });

            var entry = _context.Entry(entities);
            if (entry.State < EntityState.Added)
            {
                entry.State = EntityState.Modified;
            }
            var countAffect = _context.SaveChanges();
            return Task.FromResult(countAffect);
        }

        public virtual async Task DeleteHardAsync(params object[] keyValues)
        {
            var entity = await _context.Set<TEntity>().FindAsync(keyValues);
            ValidateAndThrow(entity);
            Entities.Remove(entity);
        }

        public virtual void DeleteHard(TEntity entity)
        {
            ValidateAndThrow(entity);
            Entities.Remove(entity);
        }

        public virtual async Task<int> DeleteSoftAsync(params object[] keyValues)
        {
            var entity = await _context.Set<TEntity>().FindAsync(keyValues);
            ValidateAndThrow(entity);
            entity.Deleted = DateTime.Now.ToString("yyyyMMddHHmmss");
            return await UpdateAsync(entity);
        }

        public virtual async Task<int> DeleteSoftAsync(TEntity entity)
        {
            ValidateAndThrow(entity);
            entity.Deleted = DateTime.Now.ToString("yyyyMMddHHmmss");
            return await UpdateAsync(entity);
        }

        #endregion public

        #region private

        protected DbSet<TEntity> Entities
        {
            get
            {
                if (_entitiesDbSet == null)
                    _entitiesDbSet = _context.Set<TEntity>();
                return _entitiesDbSet;
            }
        }

        protected string GetUserNameInHttpContext()
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == ClaimTypes.Name)?.Value;
            return userName;
        }

        protected void ValidateAndThrow(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        protected void ValidateAndThrow(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException(nameof(entities));
            }
        }

        #endregion private
    }
}
