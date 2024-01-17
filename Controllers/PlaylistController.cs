using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using _DOTNET_PLAYLIST.Models;
using System.Text.Json.Serialization;

namespace _DOTNET_PLAYLIST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaylistController : ControllerBase
    {
        static private List<Artista> artistas;

        //Sugestão do ChatGPT para resolver o problema de "A possible object cycle was detected."
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            
        };

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

                Artista theWeeknd = new Artista("The Weeknd", 33);
                artistas.Add(theWeeknd);
            }
            catch (ArtistaException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine($"Sugestão de correção: {ex.SugestaoCorrecao}");
            }
            catch (AlbumException ex)
            {
                Console.WriteLine($"Erro ao adicionar álbum: {ex.Message}");
                Console.WriteLine($"Sugestão de correção: {ex.SugestaoCorrecao}");
            }
            catch (MusicaException ex)
            {
                Console.WriteLine($"Erro ao adicionar música: {ex.Message}");
                Console.WriteLine($"Sugestão de correção: {ex.SugestaoCorrecao}");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Artista>> GetTodosArtistas()
        {
            return Ok(artistas);
        }

        [HttpGet("byNome/{nome}")]
        public Artista GetArtistaByNome(string nome)
        {
            foreach (Artista a in artistas)
            {
                if (a.Nome == nome)
                    return a;
            }
            return null;
        }

        [HttpPost("addArtista/{nome}/{idade}")]
        public List<Artista> InserirArtista(string nome, int idade)
        {
            try
            {
                Artista novoArtista = new Artista(nome, idade);
                artistas.Add(novoArtista);
                return artistas;
            }
            catch (ArtistaException ex)
            {
                Console.WriteLine($"Erro ao adicionar artista: {ex.Message}");
                Console.WriteLine($"Sugestão de correção: {ex.SugestaoCorrecao}");
                return artistas; 
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro geral: {e.Message}");
                return artistas; 
            }
        }

        [HttpPut("atualizarArtista/{nome}/{novoNome}/{novaIdade}")]
        public IActionResult AtualizarArtista(string nome, string novoNome, int novaIdade)
        {
            try
            {
                Artista artista = null;

                foreach (Artista a in artistas)
                {
                    if (a.Nome == nome)
                    {
                        artista = a;
                        break;
                    }
                }

                if (artista != null)
                {
                    artista.Nome = novoNome;
                    artista.Idade = novaIdade;
                    return Ok(artista);
                }
                else
                {
                    return NotFound("Artista não encontrado");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao atualizar artista: {e.Message}");
                return StatusCode(500, "Erro interno do servidor"); 
            }
        }

        [HttpDelete("removerArtista/{nome}")]
        public IActionResult RemoverArtista(string nome)
        {
            try
            {
                Artista artistaParaRemover = null;

                foreach (Artista a in artistas)
                {
                    if (a.Nome == nome)
                    {
                        artistaParaRemover = a;
                    }
                }

                if (artistaParaRemover != null)
                {
                    artistas.Remove(artistaParaRemover);
                    return Ok(artistas);
                }
                else
                {
                    return NotFound("Artista não encontrado");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao remover artista: {e.Message}");
                return StatusCode(500, "Erro interno do servidor"); 
            }
        }

        [HttpGet("getAlbumByNome/{artistaNome}/{albumNome}")]
        public Album GetAlbumByNome(string artistaNome, string albumNome)
        {
            try
            {
                Artista artista = GetArtistaByNome(artistaNome);

                if (artista != null)
                {
                    foreach (Album album in artista.Albums)
                    {
                        if (album.Nome == albumNome)
                        {
                            return album;
                        }
                    }
                    return null; 
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao buscar álbum por nome: {e.Message}");
                return null; 
            }
        }

        [HttpGet("getAlbumsByArtista/{artistaNome}")]
        public IActionResult GetAlbumsByArtista(string artistaNome)
        {
            try
            {
                Artista artista = GetArtistaByNome(artistaNome);

                if (artista != null)
                {
                    return Ok(artista.Albums);
                }
                else
                {
                    return NotFound("Artista não encontrado");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao buscar álbuns: {e.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPost("addAlbum/{nome}/{generoAlbum}/{anoLancamento}/{artistaNome}")]
        public IActionResult InserirAlbum(string nome, string generoAlbum, int anoLancamento, string artistaNome)
        {
            try
            {
                Artista artista = GetArtistaByNome(artistaNome);

                if (artista != null)
                {
                    Album novoAlbum = new Album(nome, generoAlbum, anoLancamento, artista);
                    artista.Albums.Add(novoAlbum);

                    //Sugestão do ChatGPT para resolver o problema de "A possible object cycle was detected."
                    string json = JsonSerializer.Serialize(novoAlbum, jsonOptions);

                    return Ok(json);
                }
                else
                {
                    return NotFound("Artista não encontrado");
                }
            }
            catch (AlbumException ex)
            {
                Console.WriteLine($"Erro ao adicionar álbum: {ex.Message}");
                Console.WriteLine($"Sugestão de correção: {ex.SugestaoCorrecao}");
                return StatusCode(400, "Erro na requisição");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro geral: {e.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPut("atualizarAlbum/{artistaNome}/{albumNome}/{novoNome}/{novoGeneroAlbum}/{novoAnoLancamento}")]
        public IActionResult AtualizarAlbum(string artistaNome, string albumNome, string novoNome, string novoGeneroAlbum, int novoAnoLancamento)
        {
            try
            {
                Album album = GetAlbumByNome(artistaNome, albumNome);

                if (album != null)
                {
                    album.Nome = novoNome;
                    album.GeneroAlbum = novoGeneroAlbum;
                    album.AnoLancamento = novoAnoLancamento;

                    return Ok(album);
                }
                else
                {
                    return NotFound("Álbum não encontrado");
                }
            }
            catch (AlbumException ex)
            {
                Console.WriteLine($"Erro ao atualizar álbum: {ex.Message}");
                return StatusCode(400, "Erro na requisição");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro geral: {e.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpDelete("removerAlbum/{artistaNome}/{albumNome}")]
        public IActionResult RemoverAlbum(string artistaNome, string albumNome)
        {
            try
            {
                Artista artista = GetArtistaByNome(artistaNome);

                if (artista != null)
                {
                    Album albumParaRemover = GetAlbumByNome(artistaNome, albumNome);

                    if (albumParaRemover != null)
                    {
                        artista.Albums.Remove(albumParaRemover);
                        return Ok(artista.Albums);
                    }
                    else
                    {
                        return NotFound("Álbum não encontrado");
                    }
                }
                else
                {
                    return NotFound("Artista não encontrado");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao remover álbum: {e.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpGet("getMusicaByNome/{artistaNome}/{albumNome}/{musicaNome}")]
        public Musica GetMusicaByNome(string artistaNome, string albumNome, string musicaNome)
        {
            try
            {
                Album album = GetAlbumByNome(artistaNome, albumNome);

                if (album != null)
                {
                    foreach (Musica musica in album.Musicas)
                    {
                        if (musica.Nome == musicaNome)
                        {
                            return musica;
                        }
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao buscar música por nome: {e.Message}");
                return null;
            }
        }

        [HttpGet("getMusicasByAlbum/{artistaNome}/{albumNome}")]
        public IActionResult GetMusicasByAlbum(string artistaNome, string albumNome)
        {
            try
            {
                Album album = GetAlbumByNome(artistaNome, albumNome);

                if (album != null)
                {
                    return Ok(album.Musicas);
                }
                else
                {
                    return NotFound("Álbum não encontrado");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao buscar músicas do álbum: {e.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPost("addMusica/{nome}/{generoMusica}/{anoLancamento}/{artistaNome}/{albumNome}")]
        public IActionResult InserirMusica(string nome, string generoMusica, int anoLancamento, string artistaNome, string albumNome)
        {
            try
            {
                Album album = GetAlbumByNome(artistaNome, albumNome);

                if (album != null)
                {
                    Musica novaMusica = new Musica(nome, generoMusica, anoLancamento, album.ArtistaAlbum, album);
                    album.Musicas.Add(novaMusica);

                    //Sugestão do ChatGPT para resolver o problema de "A possible object cycle was detected."
                    string json = JsonSerializer.Serialize(novaMusica, jsonOptions);

                    return Ok(json);
                    //return Ok(novaMusica);
                }
                else
                {
                    return NotFound("Álbum não encontrado");
                }
            }
            catch (MusicaException ex)
            {
                Console.WriteLine($"Erro ao adicionar música: {ex.Message}");
                Console.WriteLine($"Sugestão de correção: {ex.SugestaoCorrecao}");
                return StatusCode(400, "Erro na requisição");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro geral: {e.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPut("atualizarMusica/{artistaNome}/{albumNome}/{musicaNome}/{novoNome}/{novoGeneroMusica}/{novoAnoLancamento}")]
        public IActionResult AtualizarMusica(string artistaNome, string albumNome, string musicaNome, string novoNome, string novoGeneroMusica, int novoAnoLancamento)
        {
            try
            {
                Musica musica = GetMusicaByNome(artistaNome, albumNome, musicaNome);

                if (musica != null)
                {
                    musica.Nome = novoNome;
                    musica.GeneroMusica = novoGeneroMusica;
                    musica.AnoLancamento = novoAnoLancamento;

                    return Ok(musica);
                }
                else
                {
                    return NotFound("Música não encontrada");
                }
            }
            catch (MusicaException ex)
            {
                Console.WriteLine($"Erro ao atualizar música: {ex.Message}");
                return StatusCode(400, "Erro na requisição");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro geral: {e.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpDelete("removerMusica/{artistaNome}/{albumNome}/{musicaNome}")]
        public IActionResult RemoverMusica(string artistaNome, string albumNome, string musicaNome)
        {
            try
            {
                Album album = GetAlbumByNome(artistaNome, albumNome);

                if (album != null)
                {
                    Musica musicaParaRemover = GetMusicaByNome(artistaNome, albumNome, musicaNome);

                    if (musicaParaRemover != null)
                    {
                        album.Musicas.Remove(musicaParaRemover);
                        return Ok(album.Musicas);
                    }
                    else
                    {
                        return NotFound("Música não encontrada");
                    }
                }
                else
                {
                    return NotFound("Álbum não encontrado");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao remover música: {e.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}