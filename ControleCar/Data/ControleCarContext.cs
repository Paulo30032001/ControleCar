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

        public DbSet<usuario> usuario { get; set; }

        public DbSet<cliente> cliente { get; set; }

        public DbSet<formas_pagamento> formas_pagamento { get; set; }
    }
}
