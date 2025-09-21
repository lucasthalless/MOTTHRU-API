using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Domain.Interfaces
{
    public interface IRfidRepository
    {
        Task<RfidEntity?> AdicionarAsync(RfidEntity entity);
        Task<RfidEntity?> EditarAsync(int id, RfidEntity entity);
        Task<RfidEntity?> DeletarAsync(int id);
        Task<RfidEntity?> ObterUmAsync(int id);
        Task<PageResultModel<IEnumerable<RfidEntity>>> ObterTodosAsync(int deslocamento = 0, int registrosRetornados = 10);
        Task<RfidEntity?> ObterPorMotoAsync(int motoId);
    }
}