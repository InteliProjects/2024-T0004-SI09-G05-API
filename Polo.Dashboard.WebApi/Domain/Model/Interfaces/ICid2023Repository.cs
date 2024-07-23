using System.Collections.Generic;
using System.Threading.Tasks;
using Polo.Dashboard.WebApi.Domain.DTOs;

namespace Polo.Dashboard.WebApi.Domain.Model.Interfaces
{
    public class UnidadeAtestados
    {
        public int Colaboradores { get; set; }
        public int Atestados { get; set; }
    }

    public interface ICid2023Repository
    {
        Task<List<Cid2023DTO>> GetAsync();
        Task<List<Cid2023DTO>> GetByUniAsync(string unidade);
        Task<Dictionary<string, UnidadeAtestados>> GetColaboradoresAtestadosPorUnidadeAsync();
        Task<IEnumerable<CidTotalUniDTO>> GetCertTotal();

    }
}