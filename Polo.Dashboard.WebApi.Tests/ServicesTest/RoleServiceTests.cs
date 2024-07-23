using Xunit;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Application.Services;

namespace Polo.Dashboard.WebApi.Tests.ServicesTest
{
    public class RoleServiceTests
    {
        [Fact]
        public void DetermineRole_PresidenteExecutivo_ShouldReturnPresidente()
        {
            // Arrange
            var roleService = new RoleService();
            var empregado = new EmpregadoDTO { cargo = "PRESIDENTE EXECUTIVO" };

            // Act
            var role = roleService.DetermineRole(empregado);

            // Assert
            Assert.Equal("Presidente", role);
        }

        [Fact]
        public void DetermineRole_Funcionario_ShouldReturnFuncionario()
        {
            // Arrange
            var roleService = new RoleService();
            var empregado = new EmpregadoDTO { cargo = "ANALISTA DE SISTEMAS" };

            // Act
            var role = roleService.DetermineRole(empregado);

            // Assert
            Assert.Equal("Funcionário", role);
        }
    }
}