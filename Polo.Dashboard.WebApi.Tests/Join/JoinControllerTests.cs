using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Polo.Dashboard.WebApi.Controllers.v1;

public class JoinControllerTests
{
    [Fact]
    public async Task GetJoinData_ReturnsOkResult_WithJoinDataList()
    {
        // Arrange
        var mockRepo = new Mock<IJoinRepository>();
        var mockLogger = new NullLogger<JoinController>(); // Usando NullLogger para simplificar
        var mockMapper = new Mock<IMapper>();

        var joinDataList = new List<ZenCidEmpDTO>
        {
            new ZenCidEmpDTO { mes = 1, n_pessoal = 101, departamento = "Dept1", total_sessoes = 10, cargo = "Cargo1", genero = "M", atestados = 5, dsc_detalhada = "Detalhada1" },
            new ZenCidEmpDTO { mes = 2, n_pessoal = 102, departamento = "Dept2", total_sessoes = 20, cargo = "Cargo2", genero = "F", atestados = 10, dsc_detalhada = "Detalhada2" }
        };

        mockRepo.Setup(repo => repo.GetJoin()).ReturnsAsync(joinDataList);
        mockMapper.Setup(mapper => mapper.Map<List<ZenCidEmpDTO>>(It.IsAny<object>())).Returns(joinDataList);

        var controller = new JoinController(mockRepo.Object, mockLogger, mockMapper.Object);

        // Act
        var result = await controller.GetJoinData();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<List<ZenCidEmpDTO>>(okResult.Value);
        Assert.Equal(2, model.Count);
    }
}
