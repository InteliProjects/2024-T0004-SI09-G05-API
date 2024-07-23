using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;

namespace Polo.Dashboard.WebApi.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class ZenklubController : ControllerBase
    {
        private readonly IZenklubRepository _zenklubRepository;
        private readonly ILogger<ZenklubController> _logger;
        private readonly IMapper _mapper;

        public ZenklubController(IZenklubRepository zenklubRepository, ILogger<ZenklubController> logger,
            IMapper mapper)
        {
            _zenklubRepository = zenklubRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando a obtenção dos zenklubs...");

                var zenklubs = await _zenklubRepository.GetAsync();
                var zenklubDTOS = _mapper.Map<List<ZenklubDTO>>(zenklubs);

                if (zenklubDTOS == null || zenklubDTOS.Count == 0)
                {
                    _logger.LogWarning("Nenhum zenklub encontrado.");
                    return NotFound("Nenhum zenklub encontrado.");
                }

                _logger.LogInformation($"Foram encontrados {zenklubDTOS.Count} zenklubs.");
                return Ok(zenklubDTOS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter zenklubs.");
                return StatusCode(500, "Ocorreu um erro ao obter zenklubs.");
            }
        }
    }
}
