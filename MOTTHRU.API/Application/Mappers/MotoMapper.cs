using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Application.Mappers
{
    public static class MotoMapper
    {
        public static MotoEntity ToMotoEntity(this MotoDto obj)
        {
            return new MotoEntity
            {
                Placa = obj.Placa,
                Chassi = obj.Chassi,
                NumMotor = obj.NumMotor,
                PatioId = obj.PatioId,
                // ModeloId = obj.ModeloId,
                // Status = obj.Status
            };
        }
    }
}