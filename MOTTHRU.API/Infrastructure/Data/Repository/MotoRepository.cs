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

        public async Task<MotoEntity?> AdicionarAsync(MotoEntity entity)
        {
            _context.Moto.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<MotoEntity?> DeletarAsync(int id)
        {
            var result = await _context.Moto.FindAsync(id);

            if (result is not null)
            {
                _context.Remove(result);
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<MotoEntity?> EditarAsync(int id, MotoEntity entity)
        {
            var result = await _context
                .Moto
                // .Include(x => x.Modelo)
                .Include(x => x.Patio)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is not null)
            {
                result.Placa = entity.Placa;
                result.Chassi = entity.Chassi;
                result.NumMotor = entity.NumMotor;
                // result.ModeloId = entity.ModeloId;
                result.PatioId = entity.PatioId;

                _context.Update(result);
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<PageResultModel<IEnumerable<MotoEntity>>> ObterTodosAsync(int deslocamento = 0, int registrosRetornados = 10)
        {
            var totalRegistros = await _context.Moto.CountAsync();

            var result = await _context
                .Moto
                // .Include(x => x.Modelo)
                .Include(x => x.Patio)
                .OrderBy(x => x.Id)
                .Skip(deslocamento)
                .Take(registrosRetornados)
                .ToListAsync();

            return new PageResultModel<IEnumerable<MotoEntity>>
            {
                Data = result,
                Deslocamento = deslocamento,
                RegistrosRetornado = registrosRetornados,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<MotoEntity?> ObterUmAsync(int id)
        {
            var result = await _context
                .Moto
                // .Include(x => x.Modelo)
                .Include(x => x.Patio)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<IEnumerable<MotoEntity>> ObterPorPatioAsync(int patioId)
        {
            return await _context.Moto
                .Where(m => m.PatioId == patioId)
                // .Include(x => x.Modelo)
                .Include(x => x.Patio)
                .ToListAsync();
        }

        // public async Task<IEnumerable<MotoEntity>> ObterPorStatusAsync(string status)
        // {
        //     return await _context.Moto
        //         // .Where(m => m.Status == status)
        //         // .Include(x => x.Modelo)
        //         .Include(x => x.Patio)
        //         .ToListAsync();
        // }
    }
}
