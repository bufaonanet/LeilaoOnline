using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.LeilaoOnline.WebApp.Services.Handles
{
    public class ArquivamentoAdminService : IAdminService
    {
        private readonly IAdminService _defaultservice;

        public ArquivamentoAdminService(ILeilaoDao leilaoDao,
            ICategoriaDao categoriaDao)
        {
            _defaultservice = new DefaultAdminService(leilaoDao, categoriaDao);
        }

        public void CadastraLeilao(Leilao leilao)
        {
            _defaultservice.CadastraLeilao(leilao);
        }

        public IEnumerable<Categoria> ConsultaCategorias()
        {
            return _defaultservice.ConsultaCategorias();
        }

        public Leilao ConsultaLeilaoPorId(int id)
        {
            return _defaultservice.ConsultaLeilaoPorId(id);
        }

        public IEnumerable<Leilao> ConsultaLeiloes()
        {
            return _defaultservice
                .ConsultaLeiloes()
                .Where(l => l.Situacao != SituacaoLeilao.Arquivado);
        }

        public IEnumerable<Leilao> ConsultaLeiloesPorTermo(string termo)
        {
            return _defaultservice
                .ConsultaLeiloesPorTermo(termo)
                .Where(l => l.Situacao != SituacaoLeilao.Arquivado);
        }

        public void FinalizaPregaoDoLeilaoComId(int id)
        {
            _defaultservice.FinalizaPregaoDoLeilaoComId(id);
        }

        public void IniciaPregaoDoLeilaoComId(int id)
        {
            _defaultservice.IniciaPregaoDoLeilaoComId(id);
        }

        public void ModificaLeilao(Leilao leilao)
        {
            _defaultservice.ModificaLeilao(leilao);
        }

        public void RemoveLeilao(Leilao leilao)
        {
            if (leilao != null && leilao.Situacao != SituacaoLeilao.Pregao)
            {
                leilao.Situacao = SituacaoLeilao.Arquivado;
                _defaultservice.ModificaLeilao(leilao);
            }
        }
    }
}
