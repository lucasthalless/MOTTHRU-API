using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Domain.Interfaces
{
    public interface IMotoRepository
    {
        IEnumerable<MotoEntity> GetAll();
        MotoEntity GetById(int id);
        IEnumerable<MotoEntity> GetByIdPatio(string idPatio);
        IEnumerable<MotoEntity> GetByStatus(string status);
        MotoEntity Create(MotoEntity item);
        MotoEntity Update(MotoEntity item);
        MotoEntity Delete(int id);
    }
}
