using Microsoft.EntityFrameworkCore;

namespace blog_core.interfaces
{
    public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task UpdateAsync(int id, T entity);
    Task DeleteAsync(int id);
}
}

// EF implementation of Repository

/*public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly DbContext _dbContext;

    public EfRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(int id, T entity)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}*/

