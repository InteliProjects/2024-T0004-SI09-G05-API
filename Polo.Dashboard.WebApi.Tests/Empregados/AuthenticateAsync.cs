using Microsoft.AspNetCore.Mvc;
using Moq;
using Polo.Dashboard.WebApi.Controllers.v1;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using Polo.Dashboard.WebApi.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Polo.Dashboard.WebApi.Controllers.v2;

namespace Polo.Dashboard.WebApi.Tests.Empregados
{
    public class AuthControllerTests
    {
        [Fact(DisplayName = "Given an employee number and role, return UserNotFound ")]
        public async Task AuthenticateAsync_ReturnsBadRequest_WhenUserNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IEmpregadosRepository>();
            var mockRoleService = new Mock<IRoleService>();

            mockRepo.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<EmpregadoDTO>());

            var controller = new AuthController(mockRepo.Object, mockRoleService.Object);

            // Act
            var result = await controller.AuthenticateAsync(new AuthenticateRequestDTO { N_pessoal = 1 });

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}
