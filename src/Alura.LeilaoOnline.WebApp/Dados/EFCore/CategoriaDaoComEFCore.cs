using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Dados.EFCore
{
    public class CategoriaDaoComEFCore : ICategoriaDao
    {
        private readonly AppDbContext _context;

        public CategoriaDaoComEFCore(AppDbContext context)
        {
            _context = context;
        }
        public IList<Categoria> BuscarTodos()
        {
            return _context
                .Categorias
                .Include(c => c.Leiloes)
                .ToList();
        }

        public Categoria BuscarPorId(int id)
        {
            return _context.Categorias
                .Include(c => c.Leiloes)
                .First(c => c.Id == id);
        }
    }
}
