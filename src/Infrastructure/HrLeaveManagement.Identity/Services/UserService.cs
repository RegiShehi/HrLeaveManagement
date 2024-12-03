using HrLeaveManagement.Application.Contracts.Identity;
using HrLeaveManagement.Application.Models.Identity;
using HrLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HrLeaveManagement.Identity.Services;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<Employee?> GetEmployee(string userId)
    {
        var employee = await userManager.FindByIdAsync(userId);

        if (employee == null) return null;

        return new Employee
        {
            Email = employee.Email ?? string.Empty,
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        };
    }

    public async Task<List<Employee>> GetEmployees()
    {
        var employees = await userManager.GetUsersInRoleAsync("Employee");

        return employees.Select(q => new Employee
        {
            Id = q.Id,
            Email = q.Email ?? string.Empty,
            FirstName = q.FirstName,
            LastName = q.LastName
        }).ToList();
    }
}