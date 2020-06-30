using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Dados
{
    public interface ILeilaoDao : IQuery<Leilao>, ICommand<Leilao>
    {
        public IList<Leilao> BuscarTodosPeloTermo(string termo);
    }
}
