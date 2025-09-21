using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Application.Interfaces;
using MOTTHRU.API.Application.Mappers;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;
using System.Net;

namespace MOTTHRU.API.Application.UseCases
{
    public class RfidUseCase : IRfidUseCase
    {
        private readonly IRfidRepository _rfidRepository;

        public RfidUseCase(IRfidRepository rfidRepository)
        {
            _rfidRepository = rfidRepository;
        }

        public async Task<OperationResult<RfidEntity?>> AdicionarRfidAsync(RfidDto entity)
        {
            try
            {
                var result = await _rfidRepository.AdicionarAsync(entity.ToRfidEntity());
                return OperationResult<RfidEntity?>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<RfidEntity?>.Failure("Não foi possível salvar o RFID", (int)HttpStatusCode.BadRequest);
            }
        }

        public async Task<OperationResult<RfidEntity?>> DeletarRfidAsync(int Id)
        {
            try
            {
                var result = await _rfidRepository.DeletarAsync(Id);

                if (result is null)
                    return OperationResult<RfidEntity?>.Failure("RFID não encontrado", (int)HttpStatusCode.NotFound);

                return OperationResult<RfidEntity?>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<RfidEntity?>.Failure("Não foi possível deletar o RFID", (int)HttpStatusCode.BadRequest);
            }
        }

        public async Task<OperationResult<RfidEntity?>> EditarRfidAsync(int Id, RfidDto entity)
        {
            try
            {
                var result = await _rfidRepository.EditarAsync(Id, entity.ToRfidEntity());

                if (result is null)
                    return OperationResult<RfidEntity?>.Failure("RFID não encontrado", (int)HttpStatusCode.NotFound);

                return OperationResult<RfidEntity?>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<RfidEntity?>.Failure("Não foi possível editar o RFID", (int)HttpStatusCode.BadRequest);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<RfidEntity>>>> ObterTodosRfidsAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var result = await _rfidRepository.ObterTodosAsync(Deslocamento, RegistrosRetornado);

            if (!result.Data.Any())
                return OperationResult<PageResultModel<IEnumerable<RfidEntity>>>.Failure("Nenhum RFID encontrado", (int)HttpStatusCode.NoContent);

            return OperationResult<PageResultModel<IEnumerable<RfidEntity>>>.Success(result);
        }

        public async Task<OperationResult<RfidEntity?>> ObterUmRfidAsync(int Id)
        {
            var result = await _rfidRepository.ObterUmAsync(Id);

            if (result is null)
                return OperationResult<RfidEntity?>.Failure("RFID não encontrado", (int)HttpStatusCode.NotFound);

            return OperationResult<RfidEntity?>.Success(result);
        }
    }
}
