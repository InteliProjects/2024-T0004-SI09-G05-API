using Xunit;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Tests.Cid2023
{
    public class Cid2023RepositoryTests
    {
        [Fact(DisplayName = "Given valid Cid2023 list, should return it and greater than zero")]
        public async Task GetAsync_ShouldReturnListOfCid2023()
        {
            // Arrange
            var mockRepository = Cid2023RepositoryMock.GetCid2023Repository();
            var cid2023Repository = mockRepository.Object;

            // Act
            var result = await cid2023Repository.GetAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Cid2023DTO>>(result);
            Assert.True(result.Count > 0);
        }
    }
}
