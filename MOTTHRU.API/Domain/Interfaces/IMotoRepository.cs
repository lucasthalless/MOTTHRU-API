using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Domain.Interfaces
{
    public interface IMotoRepository
    {
        IEnumerable<MotoEntity> GetAll();
        MotoEntity GetById(int id);
        MotoEntity Create(MotoEntity item);
        MotoEntity Update(MotoEntity item);
        MotoEntity Delete(int id);
    }
}
