namespace HrLeaveManagement.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> DeleteAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> CreateAsync(T entity);
}