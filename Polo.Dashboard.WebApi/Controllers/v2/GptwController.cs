using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;

namespace Polo.Dashboard.WebApi.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class GptwController : Controller
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
        [Authorize(Policy = "PresidenteFuncionarioPolicy")]
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

    }
}