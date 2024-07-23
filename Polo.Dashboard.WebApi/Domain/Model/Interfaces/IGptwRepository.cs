using Polo.Dashboard.WebApi.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Domain.Model.Interfaces
{
    public interface IGptwRepository
    {
        Task<IEnumerable<LocalCountDTO>> GetCountLocal();
        Task<IEnumerable<EngajamentGptwDTO>> GetEngajament();

        Task<List<GptwDTO>> GetAsync();
        Task<List<GptwDTO>> GetByUniAsync(string unidade);

    }
}
