using System;
using System.Collections.Generic;

namespace _DOTNET_PLAYLIST.Models
{
    public class Album
    {
        private string _nome;
        private string _generoAlbum;
        private int _anoLancamento;
        private Artista _artistaAlbum;
        private List<Musica> _musicas;

        public string Nome
        {
            get { return _nome; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new AlbumException("O nome do álbum não pode ser nulo, vazio ou consistir apenas em espaços em branco.", "Insira um nome válido para o álbum.");
                }
                _nome = value;
            }
        }

        public string GeneroAlbum
        {
            get { return _generoAlbum; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new AlbumException("O gênero do álbum não pode ser nulo, vazio ou consistir apenas em espaços em branco.", "Insira um gênero válido para o álbum.");
                }
                _generoAlbum = value;
            }
        }

        public int AnoLancamento
        {
            get { return _anoLancamento; }
            set
            {
                if (value < 0)
                {
                    throw new AlbumException("O ano de lançamento do álbum não pode ser negativo.", "Insira um ano de lançamento válido para o álbum.");
                }
                else if (!int.TryParse(value.ToString(), out _))
                {
                    throw new AlbumException("O ano de lançamento do álbum deve ser um valor inteiro válido.", "Insira um valor de ano de lançamento válido.");
                }
                _anoLancamento = value;
            }
        }

        public Artista ArtistaAlbum
        {
            get { return _artistaAlbum; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "O artista do álbum não pode ser nulo.");
                }
                _artistaAlbum = value;
            }
        }

        public List<Musica> Musicas
        {
            get { return _musicas; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "A lista de músicas não pode ser nula.");
                }
                foreach (Musica musica in value)
                {
                    if (_musicas.Contains(musica))
                    {
                        throw new AlbumException("A lista de músicas não pode conter músicas duplicadas.", "Corrija a lista de músicas.");
                    }
                }
                _musicas = value;
            }
        }

        public Album()
        {

        }

        public Album(string nome, string generoAlbum, int anoLancamento, Artista artistaAlbum)
        {
            Nome = nome;
            GeneroAlbum = generoAlbum;
            AnoLancamento = anoLancamento;
            ArtistaAlbum = artistaAlbum;
            _musicas = new List<Musica>();
        }
    }
}
