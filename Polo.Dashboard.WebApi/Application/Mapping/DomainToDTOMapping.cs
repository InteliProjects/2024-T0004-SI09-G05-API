using AutoMapper;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model;

namespace Polo.Dashboard.WebApi.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Empregados, EmpregadoDTO>();
            CreateMap<Cid2023, Cid2023DTO>();
            CreateMap<Gptw, GptwDTO>();
            CreateMap<Stiba, StibaDTO>();
        }
    }
}
