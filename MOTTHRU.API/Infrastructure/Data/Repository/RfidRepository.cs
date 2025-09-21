using Microsoft.EntityFrameworkCore;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;
using MOTTHRU.API.Infrastructure.Data.AppData;

namespace MOTTHRU.API.Infrastructure.Data.Repository
{
    public class RfidRepository : IRfidRepository
    {
        private readonly ApplicationContext _context;

        public RfidRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<RfidEntity?> AdicionarAsync(RfidEntity entity)
        {
            _context.Rfid.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<RfidEntity?> EditarAsync(int id, RfidEntity entity)
        {
            var result = await _context
                .Rfid
                .Include(x => x.Moto)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is not null)
            {
                result.Sinal = entity.Sinal;
                result.MotoId = entity.MotoId;

                _context.Update(result);
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<RfidEntity?> DeletarAsync(int id)
        {
            var result = await _context.Rfid.FindAsync(id);

            if (result is not null)
            {
                _context.Remove(result);
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<RfidEntity?> ObterUmAsync(int id)
        {
            var result = await _context
                .Rfid
                .Include(x => x.Moto)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<PageResultModel<IEnumerable<RfidEntity>>> ObterTodosAsync(int deslocamento = 0, int registrosRetornados = 10)
        {
            var totalRegistros = await _context.Rfid.CountAsync();

            var result = await _context
                .Rfid
                .Include(x => x.Moto)
                .OrderBy(x => x.Id)
                .Skip(deslocamento)
                .Take(registrosRetornados)
                .ToListAsync();

            return new PageResultModel<IEnumerable<RfidEntity>>
            {
                Data = result,
                Deslocamento = deslocamento,
                RegistrosRetornado = registrosRetornados,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<RfidEntity?> ObterPorMotoAsync(int motoId)
        {
            return await _context
                .Rfid
                .Include(x => x.Moto)
                .FirstOrDefaultAsync(x => x.MotoId == motoId);
        }
    }
}
