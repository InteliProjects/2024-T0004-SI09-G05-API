using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class GptwController : ControllerBase
    {
        private readonly IGptwRepository _gptwRepository;
        private readonly ILogger<GptwController> _logger;
        private readonly IMapper _mapper;

        public GptwController(IGptwRepository gptwRepository, ILogger<GptwController> logger, IMapper mapper)
        {
            _gptwRepository = gptwRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando a obtenção dos gptw...");
                var gptw = await _gptwRepository.GetAsync();
                var gptwDTOS = _mapper.Map<List<GptwDTO>>(gptw);
                if (gptwDTOS == null || gptwDTOS.Count == 0)
                {
                    _logger.LogWarning("Nenhum gptw encontrado.");
                    return NotFound("Nenhum gptw encontrado.");
                }

                _logger.LogInformation($"Foram encontrados {gptwDTOS.Count} gptw.");
                return Ok(gptwDTOS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter gptw.");
                return StatusCode(500, "Ocorreu um erro ao obter gptw.");
            }
        }

        [HttpGet("gptw-quantidaderespostas")]
        public async Task<IActionResult> GetLocalCountsAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando a obtenção dos gptw...");
                var localCounts = await _gptwRepository.GetCountLocal();

                if (localCounts == null || !localCounts.Any())
                {
                    _logger.LogWarning("Nenhum gptw encontrado.");
                    return NotFound("Nenhum gptw encontrado.");
                }

                _logger.LogInformation($"Retorno quantidade {localCounts.Count()} locais.");
                return Ok(localCounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter gptw.");
                return StatusCode(500, "Ocorreu um erro ao obter gptw.");
            }
        }

        [HttpGet("engajamento")]
        public async Task<IActionResult> GetEngajamentoAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando a obtenção do engajamento...");
                var engajamento = await _gptwRepository.GetEngajament();

                if (engajamento == null || !engajamento.Any())
                {
                    _logger.LogWarning("Nenhum engajamento encontrado.");
                    return NotFound("Nenhum engajamento encontrado.");
                }

                _logger.LogInformation($"Retornou {engajamento.Count()} resultados de engajamento.");
                return Ok(engajamento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o engajamento.");
                return StatusCode(500, "Ocorreu um erro ao obter o engajamento.");
            }
        }
    }
}