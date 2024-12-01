using AutoMapper;
using Castle.Core.Logging;
using HrLeaveManagement.Application.Contracts.Logging;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HrLeaveManagement.Application.MappingProfiles;
using HrLeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HrLeaveManagement.Application.UnitTests.Feature.LeaveTypes.Queries;

public class GetLeaveTypeListQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetLeaveTypesQueryHandler>> _mockAppLogger;

    public GetLeaveTypeListQueryHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypesRepository();

        var mapperConfig = new MapperConfiguration(c => { c.AddProfile<LeaveTypeProfile>(); });

        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveTypesQueryHandler_WithNoParams_ReturnsAllLeaveTypes()
    {
        // arrange
        var handler = new GetLeaveTypesQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        // act
        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

        // assert
        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);

        // Assert.IsType<List<LeaveTypeDto>>(result);
        // Assert.Equal(3, result.Count);
    }
}