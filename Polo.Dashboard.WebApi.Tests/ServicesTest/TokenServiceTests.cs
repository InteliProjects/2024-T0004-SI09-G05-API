using Polo.Dashboard.WebApi.Application.Services;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Xunit;

namespace Polo.Dashboard.WebApi.Tests.ServicesTest
{
    public class TokenServiceTests
    {
        [Fact(DisplayName = "Given an employee and role, return a valid token")]
        public void GenerateToken_ShouldReturnTokenObject()
        {
            // Arrange
            var empregado = new EmpregadoDTO
            {
                n_pessoal = 1,
                sg_emp = "EX2",
                texto_rh = "EX2",
                centro_cst = 33333,
                centro_custo = "EX2",
                cargo = "EX2",
                data_nascimento = "01/01/2002"
            };

            string role = "Funcionário";

            // Act
            var result = TokenService.GenerateToken(empregado, role);

            // Assert
            Assert.NotNull(result);
            var tokenProperty = result.GetType().GetProperty("token");
            Assert.NotNull(tokenProperty);
            var token = tokenProperty.GetValue(result) as string;
            Assert.False(string.IsNullOrWhiteSpace(token));
        }
    }
}
