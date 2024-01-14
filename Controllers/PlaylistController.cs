using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _DOTNET_PLAYLIST.Models;

namespace _DOTNET_PLAYLIST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaylistController : ControllerBase
    {
        static private List<Artista> artistas;

        public PlaylistController()
        {
            if (artistas == null)
            {
                artistas = new List<Artista>();
                CargaInicial();
            }
        }

        void CargaInicial()
        {
            try
            {
                Artista lilPeep = new Artista("Lil Peep", 21);
                artistas.Add(lilPeep);

                Artista frankOcean = new Artista("Frank Ocean", 36);
                artistas.Add(frankOcean);

                Artista theWeekend = new Artista("The Weekend", 33);
                artistas.Add(theWeekend);
            }
            catch (ArtistaException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine($"Sugestão de correção: {ex.SugestaoCorrecao}");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        public List<Artista> getTodosArtistas()
        {
            return artistas;
        }

        [HttpGet("byNome/{nome}")]
        public IActionResult GetArtistaByNome(string nome)
        {
            foreach (Artista a in artistas)
            {
                if (a.Nome == nome)
                    return Ok(a);
            }

            return NotFound("Artista não encontrado");
        }

        [HttpPost("addArtista/{nome}/{idade}")]
        public List<Artista> InserirArtista(string nome, int idade)
        {
            Artista novoArtista = new Artista(nome, idade);
            artistas.Add(novoArtista);
            return artistas;
        }
    }
}