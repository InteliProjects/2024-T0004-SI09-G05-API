﻿using Polo.Dashboard.WebApi.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Domain.Model.Interfaces
{
    public interface IEmpregadosRepository
    {
        Task<List<EmpregadoDTO>> GetAsync();
        Task<List<EmpregadoDTO>> GetByUniAsync(string unidade);
        Task<IEnumerable<EmpregadoSegDTO>> GetSeg();

    }
}