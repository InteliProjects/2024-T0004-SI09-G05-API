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
    public class JoinController : ControllerBase
    {
        private readonly IJoinRepository _joinRepository;
        private readonly ILogger<JoinController> _logger;
        private readonly IMapper _mapper;

        public JoinController(IJoinRepository joinRepository, ILogger<JoinController> logger,
            IMapper mapper)
        {
            _joinRepository = joinRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("join-data")]
        public async Task<IActionResult> GetJoinData()
        {
            try
            {
                _logger.LogInformation("Obtendo dados do join...");
                var joinData = await _joinRepository.GetJoin();
                var joinDtos = _mapper.Map<List<ZenCidEmpDTO>>(joinData);
                if (joinDtos == null || joinDtos.Count == 0)
                {
                    _logger.LogWarning("Nenhum dado de join encontrado.");
                    return NotFound("Nenhum dado de join encontrado.");
                }

                _logger.LogInformation($"Foram encontrados {joinDtos.Count} dados de join.");
                return Ok(joinDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados de join.");
                return StatusCode(500, "Ocorreu um erro ao obter dados de join.");
            }
        }
    }
}
