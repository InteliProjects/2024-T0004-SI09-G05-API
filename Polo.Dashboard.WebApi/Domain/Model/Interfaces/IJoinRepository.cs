using Polo.Dashboard.WebApi.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Domain.Model.Interfaces
{
    public interface IJoinRepository
    {
        Task<IEnumerable<ZenCidEmpDTO>> GetJoin();

    }
}