using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Dados
{
    public interface ILeilaoDao
    {
        public IList<Categoria> GetCategorias();
        public IList<Leilao> GetLeiloes();
        public Leilao GetLeilaoById(int id);
        public void Insert(Leilao leilao);
        public void Update(Leilao leilao);
        public void Delete(Leilao leilao);
    }
}
