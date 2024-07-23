using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Polo.Dashboard.WebApi.Controllers.v1;

public class StibaControllerTests
{
    [Fact]
    public async Task GetAsync_ReturnsOkResult_WithStibaList()
    {
        // Arrange
        var mockRepo = new Mock<IStibaRepository>();
        var mockLogger = new Mock<ILogger<StibaController>>();
        var mockMapper = new Mock<IMapper>();

        var stibaList = new List<StibaDTO>
        {
            new StibaDTO { descricaoUO = "UO1", elegiveis = 10, particip = 5, notaStiba = "8.5" },
            new StibaDTO { descricaoUO = "UO2", elegiveis = 20, particip = 10, notaStiba = "9.0" }
        };

        mockRepo.Setup(repo => repo.GetAsync()).ReturnsAsync(stibaList);
        mockMapper.Setup(mapper => mapper.Map<List<StibaDTO>>(It.IsAny<object>())).Returns(stibaList);

        var controller = new StibaController(mockRepo.Object, mockLogger.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<List<StibaDTO>>(okResult.Value);
        Assert.Equal(2, model.Count);
    }
}
