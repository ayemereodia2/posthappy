using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace blog_core.interfaces
{
    public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly BlogDbContext _dbContext;

    public EfRepository(BlogDbContext dbContext)
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
}

}

/*
public class AdoNetRepository<T> : IRepository<T> where T : class
{
    private readonly string _connectionString;

    public AdoNetRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddAsync(T entity)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("INSERT INTO TableName (Column1, Column2) VALUES (@Value1, @Value2)", connection);
            // Add parameters based on entity properties
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var results = new List<T>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT * FROM TableName", connection);
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    // Map data to entity (e.g., T)
                    results.Add(MapToEntity(reader));
                }
            }
        }
        return results;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        T entity = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT * FROM TableName WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    entity = MapToEntity(reader);
                }
            }
        }
        return entity;
    }

    public async Task UpdateAsync(int id, T entity)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("UPDATE TableName SET Column1 = @Value1 WHERE Id = @Id", connection);
            // Add parameters based on entity properties
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("DELETE FROM TableName WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync();
        }
    }

    private T MapToEntity(SqlDataReader reader)
    {
        // Map the data from reader to the entity
        // Example: return new T() { Property1 = reader["Column1"].ToString() }
        return null; // Implement entity mapping logic
    }
}

*/