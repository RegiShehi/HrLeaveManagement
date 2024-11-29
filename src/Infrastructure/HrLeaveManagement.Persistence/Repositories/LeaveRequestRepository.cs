using Domain;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Persistence.DatabaseContext;

namespace HrLeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    protected LeaveRequestRepository(HrDatabaseContext context) : base(context)
    {
    }
}