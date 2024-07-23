using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StibaController : ControllerBase
    {
        private readonly IStibaRepository _stibaRepository;
        private readonly ILogger<StibaController> _logger;
        private readonly IMapper _mapper;

        public StibaController(IStibaRepository stibaRepository, ILogger<StibaController> logger,
            IMapper mapper)
        {
            _stibaRepository = stibaRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando a obtenção dos dados Stiba...");
                var stibaData = await _stibaRepository.GetAsync();
                var stibaDtos = _mapper.Map<List<StibaDTO>>(stibaData);
                if (stibaDtos == null || stibaDtos.Count == 0)
                {
                    _logger.LogWarning("Nenhum dado Stiba encontrado.");
                    return NotFound("Nenhum dado Stiba encontrado.");
                }

                _logger.LogInformation($"Foram encontrados {stibaDtos.Count} dados Stiba.");
                return Ok(stibaDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados Stiba.");
                return StatusCode(500, "Ocorreu um erro ao obter dados Stiba.");
            }
        }

        [HttpGet("media-nota")]
        public async Task<IActionResult> GetMeanGrade()
        {
            try
            {
                _logger.LogInformation("Calculando Média...");
                var averageGrade = await _stibaRepository.GetMeanGrade();

                int roundedAverageGrade = (int)Math.Round(averageGrade);

                _logger.LogInformation($"Média Stiba: {roundedAverageGrade}");
                return Ok(roundedAverageGrade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter média.");
                return StatusCode(500, "Erro ao obter média.");
            }
        }

        [HttpGet("media-local")]
        public async Task<IActionResult> GetAverageLocal()
        {
            try
            {
                _logger.LogInformation("Obtendo média por local...");
                var averageLocal = await _stibaRepository.GetAverageLocal();

                _logger.LogInformation("Média por local obtida com sucesso.");
                return Ok(averageLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter média por local.");
                return StatusCode(500, "Erro ao obter média por local.");
            }
        }
        [HttpGet("engajamento-local")]
        public async Task<IActionResult> GetEngajamentLocal()
        {
            try
            {
                _logger.LogInformation("Obtendo engajamento por local...");
                var averageLocal = await _stibaRepository.GetEngajamentLocal();

                _logger.LogInformation("Engajamento por local obtida com sucesso.");
                return Ok(averageLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter engajamento por local.");
                return StatusCode(500, "Erro ao obter engajamento por local.");
            }
        }
        [HttpGet("engajamento-total")]
        public async Task<IActionResult> GetAverageMedio()
        {
            try
            {
                _logger.LogInformation("Obtendo engajamento por local...");
                var average = await _stibaRepository.GetAverageMedio();

                _logger.LogInformation("Engajamento por local obtida com sucesso.");
                return Ok(average);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter engajamento por local.");
                return StatusCode(500, "Erro ao obter engajamento por local.");
            }
        }

    }
}
