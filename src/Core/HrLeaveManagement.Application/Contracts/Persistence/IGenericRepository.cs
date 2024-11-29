using Domain.Common;

namespace HrLeaveManagement.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAsync();
    Task<T?> GetByIdAsync(int id);
    Task DeleteAsync(T entity);
    Task UpdateAsync(T entity);
    Task CreateAsync(T entity);
}