using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Application.Interfaces
{
    public interface IMotoApplicationService
    {
        IEnumerable<MotoEntity> GetAll();
        MotoEntity GetMotoById(int id);
        MotoEntity CreateMoto(MotoDto entity);
        MotoEntity UpdateMoto(int id, MotoDto entity);
        MotoEntity DeleteMoto(int id);
    }
}
