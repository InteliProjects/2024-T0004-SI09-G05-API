using Dapper;
using Microsoft.EntityFrameworkCore;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Infraestrutura.Repositories
{
    public class JoinRepository : IJoinRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<IEnumerable<ZenCidEmpDTO>> GetJoin()
        {
            using (var connection = new NpgsqlConnection(_context.Database.GetConnectionString()))
            {
                string query = @"
                    SELECT
                        zenklub.mes,
                        zenklub.n_pessoal,
                        departamento,
                        total_sessoes,
                        cargo,
                        genero,
                        atestados,
                        descricao_detalhada as dsc_detalhada
                    FROM
                        zenklub
                    JOIN
                        empregados ON zenklub.n_pessoal = empregados.n_pessoal
                    JOIN
                        cid_f_2023_geral AS cfg ON empregados.n_pessoal = cfg.n_pessoal";

                var result = await connection.QueryAsync<ZenCidEmpDTO>(query);
                return result;
            }
        }
    }
}
