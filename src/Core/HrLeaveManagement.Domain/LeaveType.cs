using Domain.Common;

namespace Domain;

public class LeaveType : BaseEntity
{
    public required string Name { get; set; }
    public int DefaultDays { get; set; }
}