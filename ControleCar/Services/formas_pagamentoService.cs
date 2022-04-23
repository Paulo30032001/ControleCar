using System.Threading.Tasks;
using System.Collections.Generic;
using ControleCar.Models;
using ControleCar.Data;
using Microsoft.EntityFrameworkCore;
namespace ControleCar.Services
{
    public class formas_pagamentoService
    {
        private readonly ControleCarContext _context;
        public formas_pagamentoService(ControleCarContext _context)
        {
            this._context= _context;
        }

        public async Task<List<formas_pagamento>> FindAllAsync()
        {
            return await _context.formas_pagamento.ToListAsync();
        }

        public async Task InsertAsync(formas_pagamento formas_pagamento)
        {
            _context.formas_pagamento.Add(formas_pagamento);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(int id)
        {
            try
            {
                var forma = await _context.formas_pagamento.FindAsync(id);
                _context.Remove(forma);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<formas_pagamento> FindByIdAsync(int id)
        {
            return await _context.formas_pagamento.FirstOrDefaultAsync(x => x.id == id);
        }


        public async Task UpdateAsync(formas_pagamento formas_pagamento)
        {
            if (!await _context.formas_pagamento.AnyAsync(x => x.id == formas_pagamento.id))
            {
                throw new Exception("Identificador  Não  encontrado");
            }
            try
            {
                _context.Update(formas_pagamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }

        }


    }
}
