using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;
using System;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    public class LeilaoController : Controller
    {
        private readonly LeilaoDao _dao;
        private readonly AppDbContext _context;

        public LeilaoController()
        {
            _context = new AppDbContext();
            _dao = new LeilaoDao();
        }

        public IActionResult Index()
        {
            var leiloes = _dao.GetLeiloes();
            return View(leiloes);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            ViewData["Categorias"] = _dao.GetCategorias();
            ViewData["Operacao"] = "Inclusão";
            return View("Form");
        }

        [HttpPost]
        public IActionResult Insert(Leilao model)
        {
            if (ModelState.IsValid)
            {
                _dao.Insert(model);
                return RedirectToAction("Index");
            }
            ViewData["Categorias"] = _dao.GetCategorias();
            ViewData["Operacao"] = "Inclusão";
            return View("Form", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categorias"] = _dao.GetCategorias();
            ViewData["Operacao"] = "Edição";

            var leilao = _dao.GetLeilaoById(id);

            if (leilao == null) return NotFound();
            return View("Form", leilao);
        }

        [HttpPost]
        public IActionResult Edit(Leilao model)
        {
            if (ModelState.IsValid)
            {
                _dao.Update(model);
                return RedirectToAction("Index");
            }
            ViewData["Categorias"] = _dao.GetCategorias();
            ViewData["Operacao"] = "Edição";
            return View("Form", model);
        }

        [HttpPost]
        public IActionResult Inicia(int id)
        {
            var leilao = _dao.GetLeilaoById(id);

            if (leilao == null) return NotFound();
            if (leilao.Situacao != SituacaoLeilao.Rascunho)
                return StatusCode(405);

            leilao.Situacao = SituacaoLeilao.Pregao;
            leilao.Inicio = DateTime.Now;

            _dao.Update(leilao);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Finaliza(int id)
        {
            var leilao = _dao.GetLeilaoById(id);

            if (leilao == null)
                return NotFound();

            if (leilao.Situacao != SituacaoLeilao.Pregao)
                return StatusCode(405);

            leilao.Situacao = SituacaoLeilao.Finalizado;
            leilao.Termino = DateTime.Now;

            _dao.Update(leilao);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var leilao = _dao.GetLeilaoById(id);

            if (leilao == null)
                return NotFound();

            if (leilao.Situacao == SituacaoLeilao.Pregao)
                return StatusCode(405);

            _dao.Delete(leilao);

            return NoContent();
        }

        [HttpGet]
        public IActionResult Pesquisa(string termo)
        {
            ViewData["termo"] = termo;
            var leiloes = _context.Leiloes
                .Include(l => l.Categoria)
                .Where(l => string.IsNullOrWhiteSpace(termo) ||
                    l.Titulo.ToUpper().Contains(termo.ToUpper()) ||
                    l.Descricao.ToUpper().Contains(termo.ToUpper()) ||
                    l.Categoria.Descricao.ToUpper().Contains(termo.ToUpper())
                );
            return View("Index", leiloes);
        }
    }
}
