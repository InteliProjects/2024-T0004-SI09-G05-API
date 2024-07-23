using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using System.Globalization;


namespace Polo.Dashboard.WebApi.Infraestrutura.Repositories
{
    public class StibaRepository : IStibaRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public async Task<List<StibaDTO>> GetAsync()
        {
            return _context.Stiba.ToList()
                .Select(b =>
                    new StibaDTO()
                    {
                        descricaoUO = b.descricaoUO,
                        elegiveis = b.elegiveis,
                        particip = b.particip,
                        notaStiba = b.notaStiba
                    })
                .ToList();
        }


        public async Task<double> GetMeanGrade()
        {
            var stibaData = await _context.Stiba.ToListAsync();

            if (stibaData.Count == 0)
            {
                return 0;
            }

            var grades = stibaData.Select(item =>
            {
                double grade;
                if (double.TryParse(item.notaStiba.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out grade))
                {
                    return grade;
                }
                else
                {
                    return 0;
                }
            });

            double averageGrade = grades.Sum() / grades.Count();

            return averageGrade;
        }


        public async Task<IEnumerable<StibaAverageDTO>> GetAverageLocal()
        {
            using (var connection = new NpgsqlConnection(_context.Database.GetConnectionString()))
            {
                string query = @"
            SELECT 
              CASE 
                WHEN ""Descrição UO"" LIKE '%ANCHIETA%' THEN 'ANC'
                WHEN ""Descrição UO"" LIKE '%TAUBATE%' THEN 'TAU'
                WHEN ""Descrição UO"" LIKE '%TBT%' THEN 'TAU'
                WHEN ""Descrição UO"" LIKE '%SAO CARLOS%' THEN 'SCA'
                ELSE ""Descrição UO""
              END AS ""Sigla_UO"",
              AVG(CAST(REPLACE(""Nota Stiba"", ',', '.') AS float)) AS average_nota_stiba
            FROM stiba_2023 
            WHERE ""Descrição UO"" LIKE '%ANCHIETA%'
               OR ""Descrição UO"" LIKE '%TAUBATE%'
               OR ""Descrição UO"" LIKE '%SAO CARLOS%'
            GROUP BY ""Sigla_UO""";

                var result = await connection.QueryAsync<StibaAverageDTO>(query);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<EmpregadosSegDTO>> GetEngajamentLocal()
        {
            using (var connection = new NpgsqlConnection(_context.Database.GetConnectionString()))
            {
                string query = @"
            SELECT 
              CASE 
                WHEN ""Descrição UO"" LIKE '%ANCHIETA%' THEN 'ANC'
                WHEN ""Descrição UO"" LIKE '%TAUBATE%' THEN 'TAU'
                WHEN ""Descrição UO"" LIKE '%TBT%' THEN 'TAU'
                WHEN ""Descrição UO"" LIKE '%SAO CARLOS%' THEN 'SCA'
                ELSE ""Descrição UO""
              END AS ""Sigla_UO"",
              AVG(""% particip"") AS average_engajamento,
              STDDEV(""% particip"") AS std_engajamento
            FROM stiba_2023 
            WHERE ""Descrição UO"" LIKE '%ANCHIETA%'
               OR ""Descrição UO"" LIKE '%TAUBATE%'
               OR ""Descrição UO"" LIKE '%SAO CARLOS%'
            GROUP BY ""Sigla_UO""";

                var result = await connection.QueryAsync<EmpregadosSegDTO>(query);
                return result.ToList();
            }
        }
        public async Task<IEnumerable<StibaAverageTotalDTO>> GetAverageMedio()
        {
            using (var connection = new NpgsqlConnection(_context.Database.GetConnectionString()))
            {
                string query = @"
          SELECT AVG(""% particip"")  FROM stiba_2023 ";

                var result = await connection.QueryAsync<StibaAverageTotalDTO>(query);
                return result.ToList();
            }
        }


    }
}
