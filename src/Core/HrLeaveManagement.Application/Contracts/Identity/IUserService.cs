using HrLeaveManagement.Application.Models.Identity;

namespace HrLeaveManagement.Application.Contracts.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee?> GetEmployee(string userId);
}