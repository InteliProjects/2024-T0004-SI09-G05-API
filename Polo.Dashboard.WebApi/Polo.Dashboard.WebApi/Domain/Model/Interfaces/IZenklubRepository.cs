using Polo.Dashboard.WebApi.Domain.DTOs;

namespace Polo.Dashboard.WebApi.Domain.Model.Interfaces
{
    public interface IZenklubRepository
    {
        Task<List<ZenklubDTO>> GetAsync();
    }
}
