using Polo.Dashboard.WebApi.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Tests.Empregados
{
    public class EmpregadosRepositoryTests
    {
        [Fact(DisplayName = "Given valid Employee list, should return it and greater than zero" )]
        public async Task GetAsync_ShouldReturnListOfEmpregados()
        {
            // Arrange
            var mockRepository = EmpregadosRepositoryMock.GetEmpregadosRepository();
            var empregadosRepository = mockRepository.Object;

            // Act
            var result = await empregadosRepository.GetAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<EmpregadoDTO>>(result);
            Assert.True(result.Count > 0);
        }
    }
}
