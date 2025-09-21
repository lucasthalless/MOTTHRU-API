using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Application.Interfaces
{
    public interface IMotoUseCase
    {
        Task<OperationResult<MotoEntity?>> AdicionarMotoAsync(MotoDto entity);
        Task<OperationResult<MotoEntity?>> EditarMotoAsync(int Id, MotoDto entity);
        Task<OperationResult<MotoEntity?>> DeletarMotoAsync(int Id);
        Task<OperationResult<MotoEntity?>> ObterUmaMotoAsync(int Id);
        Task<OperationResult<PageResultModel<IEnumerable<MotoEntity>>>> ObterTodasMotosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
    }
}