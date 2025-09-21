using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Application.Mappers
{
    public static class RfidMapper
    {
        public static RfidEntity ToRfidEntity(this RfidDto obj)
        {
            return new RfidEntity
            {
                Sinal = obj.Sinal,
                MotoId = obj.MotoId
            };
        }
    }
}