using Moq;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;

public class EmpregadosRepositoryMock
{
    public static Mock<IEmpregadosRepository> GetEmpregadosRepository()
    {
        var mockRepository = new Mock<IEmpregadosRepository>();
        var empregados = new List<EmpregadoDTO>
        {
            new EmpregadoDTO
            {
                n_pessoal = 0,
                sg_emp = "EX",
                texto_rh = "EX",
                centro_cst = 1222,
                centro_custo = "EX",
                cargo = "EX",
                data_nascimento = "01/01/2001"
            },
            new EmpregadoDTO
            {
                n_pessoal = 1,
                sg_emp = "EX2",
                texto_rh = "EX2",
                centro_cst = 33333,
                centro_custo = "EX2",
                cargo = "EX2",
                data_nascimento = "01/01/2002"
            }
        };

        mockRepository.Setup(r => r.GetAsync()).ReturnsAsync(empregados);

        return mockRepository;
    }
}