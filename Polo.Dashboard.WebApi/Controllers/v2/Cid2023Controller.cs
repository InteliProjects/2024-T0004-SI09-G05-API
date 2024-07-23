using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class Cid2023Controller : ControllerBase
    {
        private readonly ICid2023Repository _cid2023Repository;
        private readonly ILogger<Cid2023Controller> _logger;
        private readonly IMapper _mapper;
        private readonly IEmpregadosRepository _empregadosRepository;

        public Cid2023Controller(ICid2023Repository cid2023Repository, ILogger<Cid2023Controller> logger,
            IMapper mapper, IEmpregadosRepository empregadosRepository)
        {
            _cid2023Repository = cid2023Repository;
            _logger = logger;
            _mapper = mapper;
            _empregadosRepository = empregadosRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando a obtenção dos cid2023...");

                var userIdentity = User.Identity as ClaimsIdentity;
                var unidadeClaim = userIdentity?.FindFirst(ClaimTypes.UserData);
                var unidade = unidadeClaim?.Value;

                var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                List<Cid2023DTO> cid2023;
                if (userRole == "Presidente")
                {
                    cid2023 = await _cid2023Repository.GetAsync();
                }
                else
                {
                    cid2023 = await _cid2023Repository.GetByUniAsync(unidade);
                }

                var cid2023DTOS = _mapper.Map<List<Cid2023DTO>>(cid2023);
                if (cid2023DTOS == null || cid2023DTOS.Count == 0)
                {
                    _logger.LogWarning("Nenhum cid2023 encontrado.");
                    return NotFound("Nenhum cid2023 encontrado.");
                }

                _logger.LogInformation($"Foram encontrados {cid2023DTOS.Count} cid2023.");
                return Ok(cid2023DTOS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter cid2023.");
                return StatusCode(500, "Ocorreu um erro ao obter cid2023.");
            }
        }

        [Authorize]
        [HttpGet("colaboradores-atestados-por-unidade")]
        public async Task<IActionResult> GetColaboradoresAtestadosPorUnidadeAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando a obtenção de colaboradores e atestados por unidade...");
                var colaboradoresAtestadosPorUnidade = await _cid2023Repository.GetColaboradoresAtestadosPorUnidadeAsync();

                if (colaboradoresAtestadosPorUnidade == null || colaboradoresAtestadosPorUnidade.Count == 0)
                {
                    _logger.LogWarning("Nenhuma informação de colaboradores e atestados por unidade encontrada.");
                    return NotFound("Nenhuma informação de colaboradores e atestados por unidade encontrada.");
                }

                _logger.LogInformation($"Foram encontradas informações de colaboradores e atestados para {colaboradoresAtestadosPorUnidade.Count} unidades.");
                return Ok(colaboradoresAtestadosPorUnidade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter informações de colaboradores e atestados por unidade.");
                return StatusCode(500, "Ocorreu um erro ao obter informações de colaboradores e atestados por unidade.");
            }
        }
    }
}
