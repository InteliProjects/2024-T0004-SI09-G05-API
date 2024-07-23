using Polo.Dashboard.WebApi.Domain.DTOs;

namespace Polo.Dashboard.WebApi.Domain.Model.Interfaces
{
    public interface IStibaRepository
    {
        Task<IEnumerable<StibaAverageDTO>> GetAverageLocal();
        Task<IEnumerable<StibaAverageTotalDTO>> GetAverageMedio();

        Task<IEnumerable<EmpregadosSegDTO>> GetEngajamentLocal();

        Task<List<StibaDTO>> GetAsync();
        Task<double> GetMeanGrade();


    }
}
