using Microsoft.EntityFrameworkCore;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;

namespace Polo.Dashboard.WebApi.Infraestrutura.Repositories
{
    public class ZenklubRepository: IZenklubRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<List<ZenklubDTO>> GetAsync()
        {
            return  _context.Zenklub.ToList()
                .Select(b =>
                    new ZenklubDTO()
                    {
                        mes = b.mes,
                        periodo = b.periodo,
                        departamento = b.departamento,
                        total_sessoes = b.total_sessoes

                    }).ToList();
        }


    }
}
