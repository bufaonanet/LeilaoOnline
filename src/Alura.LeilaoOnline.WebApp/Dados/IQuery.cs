using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Dados
{
    public interface IQuery<T>
    {
        IList<T> BuscarTodos();
        T BuscarPorId(int id);
    }
}
