using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados
{
    public class LeilaoDao
    {
        public readonly AppDbContext _context;

        public LeilaoDao()
        {
            _context = new AppDbContext();
        }

        public IList<Categoria> GetCategorias()
        {
            return _context.Categorias.ToList();
        }

        public IList<Leilao> GetLeiloes()
        {
            return _context.Leiloes
                .Include(l => l.Categoria)
                .ToList();
        }

        public Leilao GetLeilaoById(int id)
        {
            return _context.Leiloes.FirstOrDefault(l => l.Id == id);
        }

        public void Insert(Leilao leilao)
        {
            _context.Leiloes.Add(leilao);
            _context.SaveChanges();
        }

        public void Update(Leilao leilao)
        {
            _context.Leiloes.Update(leilao);
            _context.SaveChanges();
        }

        public void Delete(Leilao leilao)
        {
            _context.Leiloes.Remove(leilao);
            _context.SaveChanges();
        }
    }
}
