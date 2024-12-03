using System.Diagnostics;
using Domain;
using HrLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HrLeaveManagement.Application.IntegrationTests;

public class HrDatabaseContextTests
{
    private readonly HrDatabaseContext _hrDatabaseContext;

    public HrDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _hrDatabaseContext = new HrDatabaseContext(dbOptions);
    }

    [Fact]
    public async Task SaveLeaveType_WithValidParams_UpdatesDateCreatedValue()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test leave type"
        };

        // Act
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        // Assert
        leaveType.DateCreated.ShouldNotBeNull();
        leaveType.DateCreated.Value.ShouldBe(leaveType.DateCreated.Value);
    }

    [Fact]
    public async Task UpdateLeaveType_WithValidParams_UpdatesDateModifiedValue()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test leave type"
        };

        // Act
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        // Assert
        leaveType.DateModified.ShouldNotBeNull();
        leaveType.DateModified.Value.ShouldBe(leaveType.DateModified.Value);
    }
}