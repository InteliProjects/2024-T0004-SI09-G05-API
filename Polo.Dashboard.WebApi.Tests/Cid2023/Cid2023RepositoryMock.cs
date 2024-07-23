using Moq;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Cid2023RepositoryMock
{
    public static Mock<ICid2023Repository> GetCid2023Repository()
    {
        var mockRepository = new Mock<ICid2023Repository>();
        var cid2023List = new List<Cid2023DTO>
        {
            new Cid2023DTO
            {
                mes = "Janeiro",
                atestados = 3,
                dias = "5",
                diretoria = "Diretoria A",
                unidade = "Unidade X",
                cid = "A123",
                descricao = "Descrição A123"
            },
            new Cid2023DTO
            {
                mes = "Fevereiro",
                atestados = 2,
                dias = "3",
                diretoria = "Diretoria B",
                unidade = "Unidade Y",
                cid = "B456",
                descricao = "Descrição B456"
            }
        };

        mockRepository.Setup(r => r.GetAsync()).ReturnsAsync(cid2023List);
        mockRepository.Setup(r => r.GetByUniAsync(It.IsAny<string>())).ReturnsAsync((string unidade) => cid2023List.FindAll(c => c.unidade == unidade));

        return mockRepository;
    }
}
