using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Polo.Dashboard.WebApi.Controllers.v1;

public class GptwControllerTests
{
    [Fact]
    public async Task GetAsync_ReturnsOkResult_WithGptwList()
    {
        // Arrange
        var mockRepo = new Mock<IGptwRepository>();
        var mockLogger = new Mock<ILogger<GptwController>>();
        var mockMapper = new Mock<IMapper>();

        var gptwList = new List<GptwDTO>
        {
            new GptwDTO { nniveis = 1, uo_abrev = "UO1", local = "Local1" },
            new GptwDTO { nniveis = 2, uo_abrev = "UO2", local = "Local2" }
        };

        mockRepo.Setup(repo => repo.GetAsync()).ReturnsAsync(gptwList);
        mockMapper.Setup(mapper => mapper.Map<List<GptwDTO>>(It.IsAny<object>())).Returns(gptwList);

        var controller = new GptwController(mockRepo.Object, mockLogger.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<List<GptwDTO>>(okResult.Value);
        Assert.Equal(2, model.Count);
    }
}
