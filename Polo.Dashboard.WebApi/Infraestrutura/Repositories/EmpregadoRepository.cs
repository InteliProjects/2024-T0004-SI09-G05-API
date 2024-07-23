using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;

namespace Polo.Dashboard.WebApi.Infraestrutura.Repositories
{
    public class EmpregadoRepository : IEmpregadosRepository

    {
        private readonly ConnectionContext _context = new();

        public async Task<List<EmpregadoDTO>> GetAsync()
        {
            return _context.Empregados.ToList()
                .Select(b =>
                    new EmpregadoDTO()
                    {
                        n_pessoal = b.n_pessoal,
                        sg_emp = b.sg_emp,
                        texto_rh = b.texto_rh,
                        centro_cst = b.centro_cst,
                        centro_custo = b.centro_custo,
                        cargo = b.cargo,
                        data_nascimento = b.data_nascimento

                    }).ToList();
        }

        public async Task<List<EmpregadoDTO>> GetByUniAsync(string unidade)
        {
            return await _context.Empregados
                .Where(e => e.texto_rh == unidade)
                .Select(b =>
                    new EmpregadoDTO
                    {
                        n_pessoal = b.n_pessoal,
                        sg_emp = b.sg_emp,
                        texto_rh = b.texto_rh,
                        centro_cst = b.centro_cst,
                        centro_custo = b.centro_custo,
                        cargo = b.cargo,
                        data_nascimento = b.data_nascimento
                    }).ToListAsync();
        }

        public async Task<IEnumerable<EmpregadoSegDTO>> GetSeg()
        {
            using (var connection = new NpgsqlConnection(_context.Database.GetConnectionString()))
            {
                        string query = @"
                   SELECT 
            CASE 
                WHEN sg_emp = 'ME' THEN 'Mensalista'
                WHEN sg_emp = 'HI' THEN 'Horista'
                WHEN sg_emp = 'EX' THEN 'Executivo'
                ELSE sg_emp 
            END AS tipo_de_empregado,
            COUNT(*) AS quantidade_de_empregados
        FROM empregados
        GROUP BY sg_emp;";

                var result = await connection.QueryAsync<EmpregadoSegDTO>(query);
                return result.ToList();
            }
        }
    }
}
