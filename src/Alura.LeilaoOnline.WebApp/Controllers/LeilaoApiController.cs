using Microsoft.AspNetCore.Mvc;
using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Dados.EFCore;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    [ApiController]
    [Route("/api/leiloes")]
    public class LeilaoApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILeilaoDao _dao;

        public LeilaoApiController()
        {
            _context = new AppDbContext();
            _dao = new LeilaoDaoComEFCore();
        }

        [HttpGet]
        public IActionResult EndpointGetLeiloes()
        {
            var leiloes = _dao.GetLeiloes();

            return Ok(leiloes);
        }

        [HttpGet("{id}")]
        public IActionResult EndpointGetLeilaoById(int id)
        {
            var leilao = _dao.GetLeilaoById(id);

            if (leilao == null)
            {
                return NotFound();
            }
            return Ok(leilao);
        }

        [HttpPost]
        public IActionResult EndpointPostLeilao(Leilao leilao)
        {
            _dao.Insert(leilao);

            return Ok(leilao);
        }

        [HttpPut]
        public IActionResult EndpointPutLeilao(Leilao leilao)
        {
            _dao.Update(leilao);
            return Ok(leilao);
        }

        [HttpDelete("{id}")]
        public IActionResult EndpointDeleteLeilao(int id)
        {
            var leilao = _dao.GetLeilaoById(id);

            if (leilao == null)
            {
                return NotFound();
            }
            _dao.Delete(leilao);

            return NoContent();
        }


    }
}
