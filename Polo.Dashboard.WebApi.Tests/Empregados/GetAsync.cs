using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Polo.Dashboard.WebApi.Controllers.v1;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using Polo.Dashboard.WebApi.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Polo.Dashboard.WebApi.Tests.Empregados
{
    public class EmpregadoControllerTests
    {
        [Fact]
        public async Task GetAsync_ReturnsOkResult_WithEmpregados()
        {
            // Arrange
            var mockRepository = new Mock<IEmpregadosRepository>();
            var empregadosDTOList = new List<EmpregadoDTO>
            {
                new EmpregadoDTO
                {
                    n_pessoal = 1,
                    sg_emp = "XXX",
                    texto_rh = "XXX",
                    centro_cst = 1,
                    centro_custo = "XXX",
                    cargo = "XXX",
                    data_nascimento = "XXX"
                },
                new EmpregadoDTO
                {
                    n_pessoal = 2,
                    sg_emp = "YYY",
                    texto_rh = "YYY",
                    centro_cst = 2,
                    centro_custo = "YYY",
                    cargo = "YYY",
                    data_nascimento = "YYY"
                }
            };

            mockRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(empregadosDTOList);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<List<EmpregadoDTO>>(It.IsAny<List<EmpregadoDTO>>()))
                .Returns((List<EmpregadoDTO> source) => source);

            var mockLogger = new Mock<ILogger<EmpregadoController>>();
            var controller = new EmpregadoController(mockRepository.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<EmpregadoDTO>>(actionResult.Value);
            Assert.NotEmpty(returnValue);
        }
    }
}