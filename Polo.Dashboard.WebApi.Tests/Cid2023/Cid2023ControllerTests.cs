using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Polo.Dashboard.WebApi.Controllers.v1;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;

using Xunit;

namespace Polo.Dashboard.WebApi.Tests.Cid2023
{
    public class Cid2023ControllerTests
    {

        [Fact(DisplayName = "Given empty Cid2023 list, should return NotFound")]
        public async Task GetAsync_ShouldReturnNotFound_WhenNoCid2023Found()
        {
            // Arrange
            var mockRepository = new Mock<ICid2023Repository>();
            mockRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Cid2023DTO>());

            var mockLogger = new Mock<ILogger<Cid2023Controller>>();
            var mockMapper = new Mock<IMapper>();

            var controller = new Cid2023Controller(mockRepository.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Nenhum cid2023 encontrado.", notFoundResult.Value);
        }

        [Fact(DisplayName = "Given an error in retrieving Cid2023, should return StatusCode 500")]
        public async Task GetAsync_ShouldReturnStatusCode500_WhenErrorOccurs()
        {
            // Arrange
            var mockRepository = new Mock<ICid2023Repository>();
            mockRepository.Setup(repo => repo.GetAsync()).ThrowsAsync(new Exception("Test exception"));

            var mockLogger = new Mock<ILogger<Cid2023Controller>>();
            var mockMapper = new Mock<IMapper>();

            var controller = new Cid2023Controller(mockRepository.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

    }
}
