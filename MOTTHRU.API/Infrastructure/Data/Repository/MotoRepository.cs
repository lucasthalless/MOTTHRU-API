using Microsoft.EntityFrameworkCore;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;
using MOTTHRU.API.Infrastructure.Data.AppData;

namespace MOTTHRU.API.Infrastructure.Data.Repository
{
    public class MotoRepository : IMotoRepository
    {
        private readonly ApplicationContext _context;

        public MotoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<MotoEntity> GetAll()
        {
            var motos = _context.Moto.ToList();
            return motos;
        }

        public MotoEntity GetById(int id)
        {
            return _context.Moto.Find(id);
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
            if (moto is null)
                throw new ArgumentNullException(nameof(moto));

            _context.Add(moto);
            _context.SaveChanges();

            return moto;
        }

        public MotoEntity Update(MotoEntity item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            var moto = _context.Moto.Find(item.id);

            if (moto is null)
                throw new InvalidOperationException("Moto não encontrada para atualização.");

            moto.chassi = item.chassi;
            moto.num_motor = item.num_motor;
            moto.placa = item.placa;

            _context.Update(moto);
            _context.SaveChanges();

            return moto;
        }

        public MotoEntity Delete(int id)
        {
            var moto = _context.Moto.Find(id);

            if (moto is null)
                throw new InvalidOperationException("Moto não encontrada para exclusão.");

            _context.Remove(moto);
            _context.SaveChanges();

            return moto;
        }
    }
}