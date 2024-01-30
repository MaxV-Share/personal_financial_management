using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Models.DbContexts;

namespace PersonalFinancialManagement.Repositories.BaseRepository;

public class BaseRepository<TEntity, TKey>(
    ApplicationDbContext context,
    IHttpContextAccessor? httpContextAccessor
)
    : IBaseRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    #region props

    protected readonly ApplicationDbContext _context = context;
    private DbSet<TEntity>? EntitiesDbSet { get; set; }
    public readonly IHttpContextAccessor? _httpContextAccessor = httpContextAccessor;

    #endregion props

    #region ctor

    public BaseRepository(ApplicationDbContext context) : this(context, null)
    {
    }

    // ReSharper disable once EmptyDestructor
    ~BaseRepository()
    {
    }

    #endregion ctor

    #region public

    public IQueryable<TEntity> GetNoTrackingEntities(
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = Entities.AsNoTracking();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        query = query.Where(e => e.CreateBy == GetUserNameInHttpContext());
        return query;
    }

    public IQueryable<TEntity> GetNoTrackingEntitiesIdentityResolution(
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = Entities.AsNoTrackingWithIdentityResolution();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        query = query.Where(e => e.CreateBy == GetUserNameInHttpContext());
        return query;
    }

    public IQueryable<TEntity> GetQueryableTable(
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = Entities.AsQueryable();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        query = query.Where(e => e.CreateBy == GetUserNameInHttpContext());
        return query;
    }

    public virtual Task<List<TEntity>> GetAllAsync(
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetNoTrackingEntities();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        var entities = query.ToListAsync();
        return entities;
    }

    public virtual Task<TEntity?> GetByIdAsync(TKey id,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = Entities.AsQueryable();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        query = query.Where(e => e.CreateBy == GetUserNameInHttpContext());
        var entity = query.SingleOrDefaultAsync(x => x.Id!.Equals(id));
        return entity;
    }

    public virtual Task<TEntity?> GetByIdNoTrackingAsync(TKey id,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetNoTrackingEntities();
        foreach (var include in includes) query = query.Include(include);
        var entity = query.SingleOrDefaultAsync(x => x.Id!.Equals(id));
        return entity;
    }

    public virtual async Task<int> CreateAsync(TEntity entity)
    {
        ValidateAndThrow(entity);
        var currentUserName = GetUserNameInHttpContext();
        entity.SetDefaultValue(currentUserName);
        await Entities.AddAsync(entity);
        var countAffect = await _context.SaveChangesAsync();
        return countAffect;
    }

    public virtual async Task<int> CreateAsync(List<TEntity> entities)
    {
        ValidateAndThrow(entities);
        var currentUserName = GetUserNameInHttpContext();
        entities.ForEach(e => { e.SetDefaultValue(currentUserName); });

        await Entities.AddRangeAsync(entities);
        var countAffect = await _context.SaveChangesAsync();
        return countAffect;
    }

    public virtual Task<int> UpdateAsync(TEntity entity)
    {
        ValidateAndThrow(entity);
        var currentUserName = GetUserNameInHttpContext();
        var entry = _context.Entry(entity);
        entity.SetValueUpdate(currentUserName);
        if (entry.State < EntityState.Added) entry.State = EntityState.Modified;
        var countAffect = _context.SaveChanges();
        return Task.FromResult(countAffect);
    }

    public virtual Task<int> UpdateAsync(IEnumerable<TEntity> entities)
    {
        var currentUserName = GetUserNameInHttpContext();
        var baseEntities = entities.ToList();
        baseEntities.ForEach(e =>
        {
            ValidateAndThrow(e);
            e.SetValueUpdate(currentUserName);
        });

        var entry = _context.Entry(baseEntities);
        if (entry.State < EntityState.Added) entry.State = EntityState.Modified;
        var countAffect = _context.SaveChanges();
        return Task.FromResult(countAffect);
    }

    public virtual async Task DeleteHardAsync(params object[] keyValues)
    {
        var entity = await _context.Set<TEntity>().FindAsync(keyValues);
        ValidateAndThrow(entity);
        Entities.Remove(entity!);
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
        entity!.Deleted = DateTime.Now.ToString("yyyyMMddHHmmss");
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

    protected DbSet<TEntity> Entities => EntitiesDbSet ??= _context.Set<TEntity>();

    protected string GetUserNameInHttpContext()
    {
        var userName = _httpContextAccessor?.HttpContext?.User
            .FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        if (userName == null)
            throw new ArgumentNullException(nameof(userName));
        return userName;
    }

    protected void ValidateAndThrow(TEntity? entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
    }

    protected void ValidateAndThrow(IEnumerable<TEntity> entities)
    {
        if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
    }

    #endregion private
}