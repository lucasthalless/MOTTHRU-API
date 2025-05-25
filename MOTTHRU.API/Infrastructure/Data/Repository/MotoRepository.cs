using Microsoft.EntityFrameworkCore;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;
using MOTTHRU.API.Infrastructure.Data.AppData;

namespace MOTTHRU.API.Infrastructure.Data.Repository
{
    public class MotoRepository: IMotoRepository
    {
        private readonly ApplicationContext _context;

        public MotoRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<MotoEntity> GetAll()
        {
            var motos =  _context.Moto.ToList();
            if (motos.Any())
                return motos;
            return null;
        }

        public MotoEntity GetById(int id)
        {
            var moto =   _context.Moto.Find(id);

            if (moto is not null)
            {
                return moto;
            }
            return null;
        }

        public IEnumerable<MotoEntity> GetByIdPatio(string idPatio)
        {
            return _context.Moto
                .Where(m => m.id_patio == idPatio)
                .ToList();
        }

        public IEnumerable<MotoEntity> GetByStatus(string status)
        {
            return _context.Moto
                .Where(m => EF.Functions.Like(m.status, status)) // ou .Equals(status) se for case-sensitive
                .ToList();
        }

        public MotoEntity Create(MotoEntity moto)
        {
            try
            {
                _context.Add(moto);
                _context.SaveChanges();
                
                return moto;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível salvar a moto.");
            }
        }

        public MotoEntity Update(MotoEntity item)
        {
            try
            {
                var moto = _context.Moto.Find(item.id);

                if (moto is not null)
                {
                    moto.chassi = item.chassi;
                    moto.num_motor = item.num_motor;
                    moto.placa = item.placa;
                    
                    _context.Update(moto);
                    _context.SaveChanges();
                    return moto;
                }

                throw new Exception("Não foi possível localizar a moto.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public MotoEntity Delete(int id)
        {
            try
            {
                var moto = _context.Moto.Find(id);

                if (moto is not null)
                {
                    _context.Remove(moto);
                    _context.SaveChanges();
                    
                    return moto;
                }
                
                throw new Exception("Não foi possível localizar a moto.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}