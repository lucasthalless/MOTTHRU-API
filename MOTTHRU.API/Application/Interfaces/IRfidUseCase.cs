using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Application.Interfaces
{
    public interface IRfidUseCase
    {
        Task<OperationResult<RfidEntity?>> AdicionarRfidAsync(RfidDto entity);
        Task<OperationResult<RfidEntity?>> EditarRfidAsync(int Id, RfidDto entity);
        Task<OperationResult<RfidEntity?>> DeletarRfidAsync(int Id);
        Task<OperationResult<RfidEntity?>> ObterUmRfidAsync(int Id);
        Task<OperationResult<PageResultModel<IEnumerable<RfidEntity>>>> ObterTodosRfidsAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
    }
}