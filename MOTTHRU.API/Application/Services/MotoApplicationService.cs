using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Application.Interfaces;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;

namespace MOTTHRU.API.Application.Services
{
    public class MotoApplicationService: IMotoApplicationService
    {
        private readonly IMotoRepository _motoRepository;

        public MotoApplicationService(IMotoRepository repository)
        {
            _motoRepository = repository;
        }

        public IEnumerable<MotoEntity> GetAll()
        {
            return _motoRepository.GetAll();
        }

        public MotoEntity GetMotoById(int id)
        {
            return _motoRepository.GetById(id);
        }
        
        public IEnumerable<MotoEntity> GetMotosByIdPatio(string idPatio)
        {
            return _motoRepository.GetByIdPatio(idPatio);
        }

        public IEnumerable<MotoEntity> GetMotosByStatus(string status)
        { 
            return _motoRepository.GetByStatus(status);
        }

        public MotoEntity CreateMoto(MotoDto moto)
        {
            var Moto = new MotoEntity
            {
                chassi = moto.chassi,
                num_motor = moto.num_motor,
                placa = moto.placa,
                status = moto.status,
            };
            
            return _motoRepository.Create(Moto);
        }

        public MotoEntity UpdateMoto(int id, MotoDto moto)
        {
            var Moto = new MotoEntity
            {
                id = id,
                chassi = moto.chassi,
                num_motor = moto.num_motor,
                placa = moto.placa,
                status = moto.status,
            };
            
            return _motoRepository.Update(Moto);
        }

        public MotoEntity DeleteMoto(int id)
        {
            return _motoRepository.Delete(id);
        }
    }
}
