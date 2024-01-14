using System;

namespace _DOTNET_PLAYLIST.Models
{
    public class Musica
    {
        private string _nome;
        private string _generoMusica;
        private int _anoLancamento;
        private Artista _artistaMusica;
        private Album _albumMusica;

        public string Nome
        {
            get { return _nome; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new MusicaException("O nome da música não pode ser nulo, vazio ou consistir apenas em espaços em branco.", "Insira um nome válido para a música.");
                }
                _nome = value;
            }
        }

        public string GeneroMusica
        {
            get { return _generoMusica; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new MusicaException("O gênero da música não pode ser nulo, vazio ou consistir apenas em espaços em branco.", "Insira um gênero válido para a música.");
                }
                _generoMusica = value;
            }
        }

        public int AnoLancamento
        {
            get { return _anoLancamento; }
            set
            {
                if (value < 0)
                {
                    throw new MusicaException("O ano de lançamento da música não pode ser negativo.", "Insira um ano de lançamento válido para a música.");
                }
                _anoLancamento = value;
            }
        }

        public Artista ArtistaMusica
        {
            get { return _artistaMusica; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "O artista da música não pode ser nulo.");
                }
                _artistaMusica = value;
            }
        }

        public Album AlbumMusica
        {
            get { return _albumMusica; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "O álbum da música não pode ser nulo.");
                }
                _albumMusica = value;
            }
        }

        public Musica()
        {

        }

        public Musica(string nome, string generoMusica, int anoLancamento, Artista artistaMusica, Album albumMusica)
        {
            Nome = nome;
            GeneroMusica = generoMusica;
            AnoLancamento = anoLancamento;
            ArtistaMusica = artistaMusica;
            AlbumMusica = albumMusica;
        }
    }
}
