using ControleCar.Data;
using System.Collections.Generic;
using ControleCar.Models;
using Microsoft.EntityFrameworkCore;
namespace ControleCar.Services
{
    public class homeService
    {
        private readonly ControleCarContext _context;

        public homeService(ControleCarContext context)
        {
            _context = context;
        }

        public async Task<int> ativos()
        {
            var usuarios = await _context.usuario.Where(x => x.ativo == true).ToListAsync();
            return usuarios.Count();
        }

        public async Task<int> inativos()
        {
            var usuarios = await _context.usuario.Where(x => x.ativo == false).ToListAsync();
            return usuarios.Count();
        }

        public async Task<int> numero_vendas(DateTime data_inicio, DateTime data_fim)
        {
            var list = await _context.vendas.Where(x => x.data >= data_inicio).Where(x => x.data < data_fim).ToListAsync();

            return list.Count();
        }


        public async Task<decimal> total_vendas(DateTime data_inicio, DateTime data_fim)
        {
            var list = await _context.vendas.Where(x => x.data >= data_inicio).Where(x => x.data < data_fim).ToListAsync();

            return list.Sum(x=>x.valor);
        }


    }
}
