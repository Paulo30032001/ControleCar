using ControleCar.Data;
using ControleCar.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace ControleCar.Services
{
    public class vendedorService
    {
        private readonly ControleCarContext _context;

        public vendedorService(ControleCarContext context)
        {
            _context = context;
        }

        public async Task<List<vendedor>> FindAllAsync()
        {
            return await _context.vendedor.ToListAsync();
        }

        public async Task InsertAsync(vendedor vendedor)
        {
            _context.vendedor.Add(vendedor);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(int id)
        {
            try
            {
                var vendedor = await _context.vendedor.FindAsync(id);
                _context.Remove(vendedor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<vendedor> FindByIdAsync(int id)
        {
            return await _context.vendedor.FirstOrDefaultAsync(x => x.id == id);
        }


        public async Task UpdateAsync(vendedor vendedor)
        {
            if (!await _context.vendedor.AnyAsync(x => x.id == vendedor.id))
            {
                throw new Exception("Identificador  Não  encontrado");
            }
            try
            {
                _context.Update(vendedor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }

        }



    }
}
