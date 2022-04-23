#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControleCar.Models;

namespace ControleCar.Data
{
    public class ControleCarContext : DbContext
    {
        public ControleCarContext (DbContextOptions<ControleCarContext> options)
            : base(options)
        {
        }

        public DbSet<ControleCar.Models.usuario> usuario { get; set; }
    }
}
