using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Application.Interfaces;
using MOTTHRU.API.Application.Mappers;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;
using System.Net;
using MOTTHRU.API.Domain.Interfaces;

namespace MOTTHRU.API.Application.UseCases
{
    public class MotoUseCase : IMotoUseCase
    {
        private readonly IMotoRepository _motoRepository;

        public MotoUseCase(IMotoRepository motoRepository)
        {
            _motoRepository = motoRepository;
        }

        public async Task<OperationResult<MotoEntity?>> AdicionarMotoAsync(MotoDto entity)
        {
            try
            {
                var result = await _motoRepository.AdicionarAsync(entity.ToMotoEntity());
                return OperationResult<MotoEntity?>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<MotoEntity?>.Failure("Não foi possível salvar a moto", (int)HttpStatusCode.BadRequest);
            }
        }

        public async Task<OperationResult<MotoEntity?>> DeletarMotoAsync(int Id)
        {
            try
            {
                var result = await _motoRepository.DeletarAsync(Id);

                if (result is null)
                    return OperationResult<MotoEntity?>.Failure("Moto não encontrada", (int)HttpStatusCode.NotFound);

                return OperationResult<MotoEntity?>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<MotoEntity?>.Failure("Não foi possível deletar a moto", (int)HttpStatusCode.BadRequest);
            }
        }

        public async Task<OperationResult<MotoEntity?>> EditarMotoAsync(int Id, MotoDto entity)
        {
            try
            {
                var result = await _motoRepository.EditarAsync(Id, entity.ToMotoEntity());

                if (result is null)
                    return OperationResult<MotoEntity?>.Failure("Moto não encontrada", (int)HttpStatusCode.NotFound);

                return OperationResult<MotoEntity?>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<MotoEntity?>.Failure("Não foi possível editar a moto", (int)HttpStatusCode.BadRequest);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<MotoEntity>>>> ObterTodasMotosAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var result = await _motoRepository.ObterTodosAsync(Deslocamento, RegistrosRetornado);

            if (!result.Data.Any())
                return OperationResult<PageResultModel<IEnumerable<MotoEntity>>>.Failure("Nenhuma moto encontrada", (int)HttpStatusCode.NoContent);

            return OperationResult<PageResultModel<IEnumerable<MotoEntity>>>.Success(result);
        }

        public async Task<OperationResult<MotoEntity?>> ObterUmaMotoAsync(int Id)
        {
            var result = await _motoRepository.ObterUmAsync(Id);

            if (result is null)
                return OperationResult<MotoEntity?>.Failure("Moto não encontrada", (int)HttpStatusCode.NotFound);

            return OperationResult<MotoEntity?>.Success(result);
        }
    }
}
