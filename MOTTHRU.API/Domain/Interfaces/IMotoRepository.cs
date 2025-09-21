using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Domain.Interfaces
{
    public interface IMotoRepository
    {
        Task<PageResultModel<IEnumerable<MotoEntity>>>
            ObterTodosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);

        Task<MotoEntity?> ObterUmAsync(int Id);
        Task<MotoEntity?> AdicionarAsync(MotoEntity entity);
        Task<MotoEntity?> EditarAsync(int Id, MotoEntity entity);
        Task<MotoEntity?> DeletarAsync(int Id);

        Task<IEnumerable<MotoEntity>> ObterPorPatioAsync(int idPatio);
        // TODO: implementar entidade "Status"
        // Task<IEnumerable<MotoEntity>> ObterPorStatusAsync(string status);
    }
}