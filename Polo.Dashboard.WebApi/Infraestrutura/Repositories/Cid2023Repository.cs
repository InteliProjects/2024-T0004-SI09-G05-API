using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;

namespace Polo.Dashboard.WebApi.Infraestrutura.Repositories
{
    public class Cid2023Repository : ICid2023Repository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public async Task<List<Cid2023DTO>> GetAsync()
        {
            return _context.Cid2023.ToList()
                .Select(b=>
                new Cid2023DTO()
                {
                    mes = b.mes,
                    atestados = b.atestados,
                    cid = b.cid,
                    descricao = b.descricao,
                    dias = b.dias,
                    diretoria = b.diretoria,
                    unidade = b.unidade

                }).ToList();
        }

        public async Task<List<Cid2023DTO>> GetByUniAsync(string unidade)
        {
            return await _context.Cid2023
                .Where(b => b.unidade == unidade)
                .Select(b => new Cid2023DTO
                {
                    mes = b.mes,
                    atestados = b.atestados,
                    cid = b.cid,
                    descricao = b.descricao,
                    dias = b.dias,
                    diretoria = b.diretoria,
                    unidade = b.unidade
                }).ToListAsync();
        }

        public async Task<IEnumerable<CidTotalUniDTO>> GetCertTotal()
        {
            using (var connection = new NpgsqlConnection(_context.Database.GetConnectionString()))
            {
                string query = @"
            SELECT mes,
                   unidade,
                   COUNT(*) AS total_atestados
            FROM cid_f_2023
            GROUP BY mes, unidade
            ORDER BY mes, unidade;
        ";

                var result = await connection.QueryAsync<CidTotalUniDTO>(query);
                return result.ToList();
            }
        }

        public async Task<Dictionary<string, UnidadeAtestados>> GetColaboradoresAtestadosPorUnidadeAsync()
        {
            var colaboradoresAtestadosPorUnidade = await _context.Cid2023
                .GroupBy(c => c.unidade)
                .Select(g => new { Unidade = g.Key, Colaboradores = g.Count(), Atestados = g.Sum(c => c.atestados) })
                .ToDictionaryAsync(x => x.Unidade, x => new UnidadeAtestados { Colaboradores = x.Colaboradores, Atestados = x.Atestados });

            return colaboradoresAtestadosPorUnidade;
        }



    }
}
