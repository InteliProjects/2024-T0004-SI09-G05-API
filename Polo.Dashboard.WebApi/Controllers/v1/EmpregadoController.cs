using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmpregadoController : ControllerBase
    {
        private readonly IEmpregadosRepository _empregadosRepository;
        private readonly ILogger<EmpregadoController> _logger;
        private readonly IMapper _mapper;

        public EmpregadoController(IEmpregadosRepository empregadosRepository, ILogger<EmpregadoController> logger, IMapper mapper)
        {
            _empregadosRepository = empregadosRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando a obtenção dos empregados...");

                var empregados = await _empregadosRepository.GetAsync();
                var empregadosDTOS = _mapper.Map<List<EmpregadoDTO>>(empregados);

                if (empregadosDTOS == null || empregadosDTOS.Count == 0)
                {
                    _logger.LogWarning("Nenhum empregado encontrado.");
                    return NotFound("Nenhum empregado encontrado.");
                }

                _logger.LogInformation($"Foram encontrados {empregadosDTOS.Count} empregados.");
                return Ok(empregadosDTOS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter empregados.");
                return StatusCode(500, "Ocorreu um erro ao obter empregados.");
            }
        }

        [HttpGet("seg")]
        public async Task<IActionResult> GetSegAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando a obtenção dos dados de segmentação...");

                var segData = await _empregadosRepository.GetSeg();

                if (segData == null)
                {
                    _logger.LogWarning("Nenhum dado de segmentação encontrado.");
                    return NotFound("Nenhum dado de segmentação encontrado.");
                }

                _logger.LogInformation($"Dados de segmentação obtidos com sucesso.");
                return Ok(segData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados de segmentação.");
                return StatusCode(500, "Ocorreu um erro ao obter dados de segmentação.");
            }
        }
    }
}