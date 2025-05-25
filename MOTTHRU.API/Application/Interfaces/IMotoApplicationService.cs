using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Application.Interfaces
{
    public interface IMotoApplicationService
    {
        IEnumerable<MotoEntity> GetAll();
        MotoEntity GetMotoById(int id);
        IEnumerable<MotoEntity> GetMotosByIdPatio(string idPatio);
        IEnumerable<MotoEntity> GetMotosByStatus(string status);
        MotoEntity CreateMoto(MotoDto entity);
        MotoEntity UpdateMoto(int id, MotoDto entity);
        MotoEntity DeleteMoto(int id);
    }
}
