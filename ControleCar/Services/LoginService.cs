using ControleCar.Data;
using System.Threading.Tasks;
using ControleCar.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleCar.Services
{
    public class LoginService
    {

        private readonly ControleCarContext _context;

        public LoginService(ControleCarContext context)
        {
            _context = context;
        }

        public async Task<usuario> FindByEmail(string email)
        {
            return await _context.usuario.FirstOrDefaultAsync(x => x.email == email);
        }
    }
}
