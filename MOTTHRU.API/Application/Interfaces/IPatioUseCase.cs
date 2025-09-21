using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Application.Interfaces
{
    public interface IPatioUseCase
    {
        Task<OperationResult<PatioEntity?>> AdicionarPatioAsync(PatioDto entity);
        Task<OperationResult<PatioEntity?>> EditarPatioAsync(int Id, PatioDto entity);
        Task<OperationResult<PatioEntity?>> DeletarPatioAsync(int Id);
        Task<OperationResult<PatioEntity?>> ObterUmPatioAsync(int Id);
        Task<OperationResult<PageResultModel<IEnumerable<PatioEntity>>>> ObterTodosPatiosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
    }
}