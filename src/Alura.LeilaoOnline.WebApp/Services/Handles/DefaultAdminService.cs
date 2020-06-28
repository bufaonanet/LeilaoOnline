using System;
using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Services.Handles
{
    public class DefaultAdminService : IAdminService
    {
        private readonly ILeilaoDao _leilaoDao;
        private readonly ICategoriaDao _categoriaDao;

        public DefaultAdminService(ILeilaoDao leilaoDao,
            ICategoriaDao categoriaDao)
        {
            _leilaoDao = leilaoDao;
            _categoriaDao = categoriaDao;
        }

        public void CadastraLeilao(Leilao leilao)
        {
            _leilaoDao.Insert(leilao);
        }

        public IEnumerable<Categoria> ConsultaCategorias()
        {
            return _categoriaDao.GetCategorias();
        }

        public Leilao ConsultaLeilaoPorId(int id)
        {
            return _leilaoDao.GetLeilaoById(id);
        }

        public IEnumerable<Leilao> ConsultaLeiloes()
        {
            return _leilaoDao.GetLeiloes();
        }

        public IEnumerable<Leilao> ConsultaLeiloesPorTermo(string termo)
        {
            return _leilaoDao.GetLeiloesByTermo(termo);
        }

        public void FinalizaPregaoDoLeilaoComId(int id)
        {
            var leilao = _leilaoDao.GetLeilaoById(id);
            if (leilao != null && leilao.Situacao == SituacaoLeilao.Pregao)
            {
                leilao.Situacao = SituacaoLeilao.Finalizado;
                leilao.Termino = DateTime.Now;
                _leilaoDao.Update(leilao);
            }
        }

        public void IniciaPregaoDoLeilaoComId(int id)
        {
            var leilao = _leilaoDao.GetLeilaoById(id);
            if (leilao != null && leilao.Situacao == SituacaoLeilao.Rascunho)
            {
                leilao.Situacao = SituacaoLeilao.Pregao;
                leilao.Inicio = DateTime.Now;
                _leilaoDao.Update(leilao);
            }
        }

        public void ModificaLeilao(Leilao leilao)
        {
            _leilaoDao.Update(leilao);
        }

        public void RemoveLeilao(Leilao leilao)
        {
            _leilaoDao.Delete(leilao);
        }
    }
}
