using ControleCar.Models;
using ControleCar.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace ControleCar.Services
{
    public class usuarioService
    {

        private readonly ControleCarContext _context;

        public usuarioService(ControleCarContext _context)
        {
            this._context = _context;
        }

        public async Task<List<usuario>> FindAllAsync()
        {
            return await _context.usuario.ToListAsync();
        }



        public async Task InsertAsync(usuario usuario)
        {
            _context.usuario.Add(usuario);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(int id)
        {
            try
            {
                var usuario = await _context.usuario.FindAsync(id);
                _context.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<usuario> FindByIdAsync(int id)
        {
            return await _context.usuario.FirstOrDefaultAsync(x => x.id == id);
        }


        public async Task UpdateAsync(usuario usuario)
        {
            if (!await _context.usuario.AnyAsync(x => x.id == usuario.id))
            {
                throw new Exception("Identificador  Não  encontrado");
            }
            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }

        }




    }
}
