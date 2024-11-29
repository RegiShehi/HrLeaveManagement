using Domain;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HrLeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    protected LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveAllocation?> GetLeaveAllocationWithDetails(int id)
    {
        var leaveAllocations = await Context.LeaveAllocations
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);

        return leaveAllocations;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        var leaveAllocations = await Context.LeaveAllocations
            .Include(q => q.LeaveType)
            .ToListAsync();

        return leaveAllocations;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        var leaveAllocations = await Context.LeaveAllocations
            .Where(q => q.EmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();

        return leaveAllocations;
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return await Context.LeaveAllocations.AnyAsync(q =>
            q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId && q.Period == period);
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await Context.LeaveAllocations.AddRangeAsync(allocations);
        await Context.SaveChangesAsync();
    }

    public async Task<LeaveAllocation?> GetUserAllocations(string userId, int leaveTypeId)
    {
        return await Context.LeaveAllocations.FirstOrDefaultAsync(q =>
            q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId);
    }
}