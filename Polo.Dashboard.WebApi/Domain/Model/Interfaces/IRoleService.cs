using Polo.Dashboard.WebApi.Domain.DTOs;
namespace Polo.Dashboard.WebApi.Domain.Model.Interfaces
{
    public interface IRoleService
    {
        string DetermineRole(EmpregadoDTO empregado);
    }
}
