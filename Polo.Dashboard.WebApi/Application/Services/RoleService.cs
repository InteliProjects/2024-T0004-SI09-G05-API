using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;

public class RoleService : IRoleService

{
    public string DetermineRole(EmpregadoDTO empregado)
    {
        switch (empregado.cargo.ToUpperInvariant())
        {
            case "PRESIDENTE EXECUTIVO":
            case "VICE-PRESIDENTE":
                return "Presidente";
            default:
                return "Funcionário";
        }
    }
}
