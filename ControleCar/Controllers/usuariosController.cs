#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleCar.Data;
using ControleCar.Models;

namespace ControleCar.Controllers
{
    public class usuariosController : Controller
    {
        private readonly ControleCarContext _context;

        public usuariosController(ControleCarContext context)
        {
            _context = context;
        }

        // GET: usuarios
        public async Task<IActionResult> index()
        {
            return View(await _context.usuario.ToListAsync());
        }

 

        // GET: usuarios/Create
        public IActionResult create()
        {
            return View();
        }

        // POST: usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create([Bind("id,email,senha,ativo,nome")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: usuarios/Edit/5
        public async Task<IActionResult> edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(int id, [Bind("id,email,senha,ativo,nome")] usuario usuario)
        {
            if (id != usuario.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.usuario.AnyAsync(x => x.id == usuario.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> delete(int? id)
        {
            if (id == null)
            {
                return Json(new { ok = false });
            }

            var usuario = await _context.usuario.FirstOrDefaultAsync(x => x.id == id);
            if (usuario == null)
            {
                return Json(new { ok = false });
            }

            try
            {
                _context.Remove(usuario);
                await _context.SaveChangesAsync();
                return Json(new { ok = true });

            }
            catch (DbUpdateConcurrencyException e)
            {
                return Json(new { ok = false, Message = e.Message });
            }

        }

    }
}
