using Xunit;
using Moq;
using Polo.Dashboard.WebApi.Controllers.v2;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Polo.Dashboard.WebApi.Tests
{
    public class AuthControllerTests
    {
        [Fact(DisplayName = "AuthenticateAsync returns BadRequest when user is not found")]
        public async Task AuthenticateAsync_ReturnsBadRequest_WhenUserNotFound()
        {
            // Arrange
            var mockEmpregadosRepository = new Mock<IEmpregadosRepository>();
            mockEmpregadosRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<EmpregadoDTO>());

            var mockRoleService = new Mock<IRoleService>();

            var controller = new AuthController(mockEmpregadosRepository.Object, mockRoleService.Object);

            // Act
            var result = await controller.AuthenticateAsync(new AuthenticateRequestDTO { N_pessoal = 999 });
            var badRequestResult = result as BadRequestObjectResult;

            // Assert
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult?.StatusCode);
        }

        // ... outros testes ...
    }
}