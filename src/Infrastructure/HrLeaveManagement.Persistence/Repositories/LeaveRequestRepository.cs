using Domain;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HrLeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository(HrDatabaseContext context)
    : GenericRepository<LeaveRequest>(context), ILeaveRequestRepository
{
    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        var leaveRequests = await Context.LeaveRequests
            .Where(q => !string.IsNullOrEmpty(q.RequestingEmployeeId))
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        var leaveRequests = await Context.LeaveRequests
            .Where(q => q.RequestingEmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }

    public async Task<LeaveRequest?> GetLeaveRequestWithDetails(int id)
    {
        var leaveRequest = await Context.LeaveRequests
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);

        return leaveRequest;
    }
}