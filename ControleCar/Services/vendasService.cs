using ControleCar.Data;
using ControleCar.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace ControleCar.Services
{
    public class vendasService
    {
        private readonly ControleCarContext _context;

        public vendasService(ControleCarContext context)
        {
            this._context = context;
        }


        public async Task<List<vendas>> FindAllAsync()
        {
            return await _context.vendas.Include(x=>x.vendedor).Include(x=>x.cliente).Include(x=>x.pecas).
                Include(x=>x.formas_pagamento).ToListAsync();
        }

      



        public async Task InsertAsync(vendas vendas)
        {
            _context.Add(vendas);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(int id)
        {
            try
            {
                var vendas = await _context.vendas.FindAsync(id);
                _context.Remove(vendas);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<vendas> FindByIdAsync(int id)
        {
            return await _context.vendas.FirstOrDefaultAsync(x => x.id == id);
        }


        public async Task UpdateAsync(vendas vendas)
        {
            if (!await _context.vendedor.AnyAsync(x => x.id == vendas.id))
            {
                throw new Exception("Identificador  Não  encontrado");
            }
            try
            {
                _context.Update(vendas);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }

        }


       





    }
}
