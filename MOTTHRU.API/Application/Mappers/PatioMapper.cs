using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Application.Mappers
{
    public static class PatioMapper
    {
        public static PatioEntity ToPatioEntity(this PatioDto obj)
        {
            return new PatioEntity
            {
                NomePatio = obj.NomePatio,
                // EnderecoId = obj.EnderecoId
            };
        }
    }
}
