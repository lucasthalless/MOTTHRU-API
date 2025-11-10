namespace MOTTHRU.API.Domain.Interfaces
{
    public interface IRfidAnomalyUseCase
    {
        Task<bool> ExecuteAsync(float sinal);
    }
}