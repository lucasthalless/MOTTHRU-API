using Microsoft.EntityFrameworkCore;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;
using MOTTHRU.API.Infrastructure.Data.AppData;

namespace MOTTHRU.API.Infrastructure.Data.Repository
{
    public class PatioRepository : IPatioRepository
    {
        private readonly ApplicationContext _context;

        public PatioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PatioEntity?> AdicionarAsync(PatioEntity entity)
        {
            _context.Patio.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PatioEntity?> EditarAsync(int id, PatioEntity entity)
        {
            var result = await _context
                .Patio
                // .Include(x => x.Endereco)
                .Include(x => x.Motos)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is not null)
            {
                result.NomePatio = entity.NomePatio;
                // result.EnderecoId = entity.EnderecoId;
                result.Motos = entity.Motos;

                _context.Update(result);
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<PatioEntity?> DeletarAsync(int id)
        {
            var result = await _context.Patio.FindAsync(id);

            if (result is not null)
            {
                _context.Remove(result);
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<PatioEntity?> ObterUmAsync(int id)
        {
            var result = await _context
                .Patio
                // .Include(x => x.Endereco)
                .Include(x => x.Motos)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<PageResultModel<IEnumerable<PatioEntity>>> ObterTodosAsync(int deslocamento = 0, int registrosRetornados = 10)
        {
            var totalRegistros = await _context.Patio.CountAsync();

            var result = await _context
                .Patio
                // .Include(x => x.Endereco)
                .Include(x => x.Motos)
                .OrderBy(x => x.Id)
                .Skip(deslocamento)
                .Take(registrosRetornados)
                .ToListAsync();

            return new PageResultModel<IEnumerable<PatioEntity>>
            {
                Data = result,
                Deslocamento = deslocamento,
                RegistrosRetornado = registrosRetornados,
                TotalRegistros = totalRegistros
            };
        }
    }
}
