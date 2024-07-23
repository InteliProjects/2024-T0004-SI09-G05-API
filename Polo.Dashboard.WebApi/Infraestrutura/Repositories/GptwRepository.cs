using Dapper;
using Microsoft.EntityFrameworkCore;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using Npgsql;

namespace Polo.Dashboard.WebApi.Infraestrutura.Repositories
{
    public class GptwRepository : IGptwRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<List<GptwDTO>> GetAsync()
        {
            return _context.Gptw.Select(b =>
                new GptwDTO()
                {
                    nniveis = b.nniveis,
                    uo_abrev = b.uo_abrev,
                    rrh = b.rrh,
                    local = b.local,
                    empr = b.empr,
                    gremp = b.gremp,
                    grpempregados = b.grpempregados,
                    sgemp = b.sgemp,
                    centrocst = b.centrocst,
                    unidorg = b.unidorg,
                    descr_uo = b.descr_uo,
                    idadedoempregado = b.idadedoempregado,
                    gn = b.gn,
                    data_adm = b.data_adm
                }).ToList();
        }

        public async Task<List<GptwDTO>> GetByUniAsync(string unidade)
        {
            return await _context.Gptw.Where(b => b.local == unidade)
                .Select(b =>
                    new GptwDTO()
                    {
                        nniveis = b.nniveis,
                        uo_abrev = b.uo_abrev,
                        rrh = b.rrh,
                        local = b.local,
                        empr = b.empr,
                        gremp = b.gremp,
                        grpempregados = b.grpempregados,
                        sgemp = b.sgemp,
                        centrocst = b.centrocst,
                        unidorg = b.unidorg,
                        descr_uo = b.descr_uo,
                        idadedoempregado = b.idadedoempregado,
                        gn = b.gn,
                        data_adm = b.data_adm
                    }).ToListAsync();
        }

        public async Task<IEnumerable<LocalCountDTO>> GetCountLocal()
        {
            using (var connection = new NpgsqlConnection(_context.Database.GetConnectionString()))
            {
                string query = "SELECT local, COUNT(*) as local_count FROM gptw GROUP BY local";

                var result = await connection.QueryAsync<LocalCountDTO>(query);
                return result.ToList();
            }

        }
        public async Task<IEnumerable<EngajamentGptwDTO>> GetEngajament()
        {
            using (var connection = new NpgsqlConnection(_context.Database.GetConnectionString()))
            {
                string query = @"
            SELECT engagement_percent
            FROM gptw_engage
            WHERE year = 2023";

                var result = await connection.QueryAsync<EngajamentGptwDTO>(query);
                return result.ToList();
            }
        }

    }
}