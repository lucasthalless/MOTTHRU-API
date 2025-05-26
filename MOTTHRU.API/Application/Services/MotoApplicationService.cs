using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Application.Interfaces;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;

namespace MOTTHRU.API.Application.Services
{
    public class MotoApplicationService : IMotoApplicationService
    {
        private readonly IMotoRepository _motoRepository;

        public MotoApplicationService(IMotoRepository repository)
        {
            _motoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<MotoEntity> GetAll()
        {
            return _motoRepository.GetAll();
        }

        public MotoEntity GetMotoById(int id)
        {
            var moto = _motoRepository.GetById(id);
            if (moto is null)
                throw new InvalidOperationException($"Moto com ID {id} não encontrada.");
            
            return moto;
        }

        public IEnumerable<MotoEntity> GetMotosByIdPatio(string idPatio)
        {
            if (string.IsNullOrWhiteSpace(idPatio))
                throw new ArgumentException("Id do pátio não pode ser vazio.");

            return _motoRepository.GetByIdPatio(idPatio);
        }

        public IEnumerable<MotoEntity> GetMotosByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status não pode ser vazio.");

            return _motoRepository.GetByStatus(status);
        }

        public MotoEntity CreateMoto(MotoDto moto)
        {
            if (moto is null)
                throw new ArgumentNullException(nameof(moto));

            var novaMoto = new MotoEntity
            {
                chassi = moto.chassi,
                num_motor = moto.num_motor,
                placa = moto.placa,
                status = moto.status,
                id_patio = moto.id_patio,
            };

            return _motoRepository.Create(novaMoto);
        }

        public MotoEntity UpdateMoto(int id, MotoDto moto)
        {
            if (moto is null)
                throw new ArgumentNullException(nameof(moto));

            var motoAtualizada = new MotoEntity
            {
                id = id,
                chassi = moto.chassi,
                num_motor = moto.num_motor,
                placa = moto.placa,
                status = moto.status,
                id_patio = moto.id_patio,
            };

            return _motoRepository.Update(motoAtualizada);
        }

        public MotoEntity DeleteMoto(int id)
        {
            return _motoRepository.Delete(id);
        }
    }
}
