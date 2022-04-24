using ControleCar.Data;
using ControleCar.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace ControleCar.Services
{
    public class pecas_departamentosService
    {
        private readonly ControleCarContext _context;

        public pecas_departamentosService(ControleCarContext _context)
        {
            this._context= _context;    
        }


        public async Task<List<pecas_departamentos>> FindAllAsync()
        {
            return await _context.pecas_departamentos.ToListAsync();
        }

        public async Task InsertAsync(pecas_departamentos pecas_departamentos)
        {
            _context.pecas_departamentos.Add(pecas_departamentos);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(int id)
        {
            try
            {
                var pecas_departamentos = await _context.pecas_departamentos.FindAsync(id);
                _context.Remove(pecas_departamentos);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<pecas_departamentos> FindByIdAsync(int id)
        {
            return await _context.pecas_departamentos.FirstOrDefaultAsync(x => x.id == id);
        }


        public async Task UpdateAsync(pecas_departamentos pecas_departamentos)
        {
            if (!await _context.pecas_departamentos.AnyAsync(x => x.id == pecas_departamentos.id))
            {
                throw new Exception("Identificador  Não  encontrado");
            }
            try
            {
                _context.Update(pecas_departamentos);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
