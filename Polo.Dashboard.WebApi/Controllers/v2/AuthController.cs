using Microsoft.AspNetCore.Mvc;
using Polo.Dashboard.WebApi.Application.Services;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Polo.Dashboard.WebApi.Controllers.v1;
using Polo.Dashboard.WebApi.Domain.DTOs;

namespace Polo.Dashboard.WebApi.Controllers.v2;

[ApiVersion("2.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IEmpregadosRepository _empregadosRepository;
    private readonly IRoleService _roleService;

    public AuthController(IEmpregadosRepository empregadosRepository, IRoleService roleService)
    {
        _empregadosRepository = empregadosRepository;
        _roleService = roleService;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateRequestDTO request)
    {
        var empregados = await _empregadosRepository.GetAsync();
        var empregado = empregados.FirstOrDefault(e => e.n_pessoal == request.N_pessoal);

        if (empregado == null) return BadRequest("Usuário não encontrado.");

        var role = _roleService.DetermineRole(empregado);

        var token = TokenService.GenerateToken(empregado, role);

        return Ok(token);
    }
}