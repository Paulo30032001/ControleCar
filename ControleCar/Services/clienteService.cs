using ControleCar.Models;
using ControleCar.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace ControleCar.Services
{
    public class clienteService
    {
        private readonly ControleCarContext _context;

        public clienteService(ControleCarContext _context)
        {
            this._context = _context;
        }

        public async Task<List<cliente>> FindAllAsync()
        {
            return await _context.cliente.ToListAsync();
        }

        public async Task InsertAsync(cliente cliente)
        {
            _context.cliente.Add(cliente);
            await _context.SaveChangesAsync();  
        }


        public async Task RemoveAsync(int id)
        {
            try
            {
                var cliente = await _context.cliente.FindAsync(id);
                _context.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<cliente> FindByIdAsync(int id)
        {
            return await _context.cliente.FirstOrDefaultAsync(x => x.id == id);
        }


        public async Task UpdateAsync(cliente cliente)
        {
            if (!await _context.cliente.AnyAsync(x => x.id == cliente.id))
            {
                throw new Exception("Identificador  Não  encontrado");
            }
            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }

        }



    }
}
