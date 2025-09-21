using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Application.Interfaces;
using MOTTHRU.API.Application.Mappers;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;
using System.Net;

namespace MOTTHRU.API.Application.UseCases
{
    public class PatioUseCase : IPatioUseCase
    {
        private readonly IPatioRepository _patioRepository;

        public PatioUseCase(IPatioRepository patioRepository)
        {
            _patioRepository = patioRepository;
        }

        public async Task<OperationResult<PatioEntity?>> AdicionarPatioAsync(PatioDto entity)
        {
            try
            {
                var result = await _patioRepository.AdicionarAsync(entity.ToPatioEntity());
                return OperationResult<PatioEntity?>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<PatioEntity?>.Failure("Não foi possível salvar o pátio", (int)HttpStatusCode.BadRequest);
            }
        }

        public async Task<OperationResult<PatioEntity?>> DeletarPatioAsync(int Id)
        {
            try
            {
                var result = await _patioRepository.DeletarAsync(Id);

                if (result is null)
                    return OperationResult<PatioEntity?>.Failure("Pátio não encontrado", (int)HttpStatusCode.NotFound);

                return OperationResult<PatioEntity?>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<PatioEntity?>.Failure("Não foi possível deletar o pátio", (int)HttpStatusCode.BadRequest);
            }
        }

        public async Task<OperationResult<PatioEntity?>> EditarPatioAsync(int Id, PatioDto entity)
        {
            try
            {
                var result = await _patioRepository.EditarAsync(Id, entity.ToPatioEntity());

                if (result is null)
                    return OperationResult<PatioEntity?>.Failure("Pátio não encontrado", (int)HttpStatusCode.NotFound);

                return OperationResult<PatioEntity?>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<PatioEntity?>.Failure("Não foi possível editar o pátio", (int)HttpStatusCode.BadRequest);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<PatioEntity>>>> ObterTodosPatiosAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var result = await _patioRepository.ObterTodosAsync(Deslocamento, RegistrosRetornado);

            if (!result.Data.Any())
                return OperationResult<PageResultModel<IEnumerable<PatioEntity>>>.Failure("Nenhum pátio encontrado", (int)HttpStatusCode.NoContent);

            return OperationResult<PageResultModel<IEnumerable<PatioEntity>>>.Success(result);
        }

        public async Task<OperationResult<PatioEntity?>> ObterUmPatioAsync(int Id)
        {
            var result = await _patioRepository.ObterUmAsync(Id);

            if (result is null)
                return OperationResult<PatioEntity?>.Failure("Pátio não encontrado", (int)HttpStatusCode.NotFound);

            return OperationResult<PatioEntity?>.Success(result);
        }
    }
}
