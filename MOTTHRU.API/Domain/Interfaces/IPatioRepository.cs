using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Domain.Interfaces
{
    public interface IPatioRepository
    {
        Task<PatioEntity?> AdicionarAsync(PatioEntity entity);
        Task<PatioEntity?> EditarAsync(int id, PatioEntity entity);
        Task<PatioEntity?> DeletarAsync(int id);
        Task<PatioEntity?> ObterUmAsync(int id);
        Task<PageResultModel<IEnumerable<PatioEntity>>> ObterTodosAsync(int deslocamento = 0, int registrosRetornados = 10);
    }
}