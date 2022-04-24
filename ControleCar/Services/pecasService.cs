using ControleCar.Data;
using ControleCar.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace ControleCar.Services
{
    public class pecasService
    {
        private readonly ControleCarContext _context;

        public pecasService(ControleCarContext context)
        {
            _context = context;
        }

        public async Task<List<pecas>> FindAllAsync()
        {
            return await _context.pecas.Include(x=>x.pecas_departamentos).ToListAsync();
        }

        public async Task InsertAsync(pecas pecas)
        {
            _context.pecas.Add(pecas);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(int id)
        {
            try
            {
                var pecas = await _context.pecas.FindAsync(id);
                _context.Remove(pecas);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<pecas> FindByIdAsync(int id)
        {
            return await _context.pecas.FirstOrDefaultAsync(x => x.id == id);
        }


        public async Task UpdateAsync(pecas pecas)
        {
            if (!await _context.pecas.AnyAsync(x => x.id == pecas.id))
            {
                throw new Exception("Identificador  Não  encontrado");
            }
            try
            {
                _context.Update(pecas);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }

        }








    }
}
