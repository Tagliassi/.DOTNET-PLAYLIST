using System;
using System.Collections.Generic;

namespace _DOTNET_PLAYLIST.Models{

    public class Artista{
        private string _nome;
        private int _idade;
        private List<Album> _albums;
        private List<Musica> _musicas;

        public string Nome
        {
            get {return _nome;}
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArtistaException("O nome do artista não pode ser nulo, vazio ou consistir apenas em espaços em branco.", "Insira um nome válido para o artista.");
                }
                _nome = value;
            }
        }

        public int Idade
        {
            get{return _idade;}
            set
            {
                if (value < 0){
                    throw new ArtistaException("A idade do artista não pode ser negativa.", "Insira uma idade válida para o artista.");
                }
                else if (!int.TryParse(value.ToString(), out _)){
                    throw new ArtistaException("A idade do artista deve ser um valor inteiro válido.", "Insira um valor de idade válido.");
                }
                _idade = value;
            }
        }

        public List<Album> Albums
        {
            get{return _albums;}
            set{
                if (value == null){
                    throw new ArgumentNullException(nameof(value), "A lista de álbuns não pode ser nula.");
                }
                foreach (Album album in value)
                {
                    if (_albums.Contains(album))
                    {
                        throw new ArtistaException("A lista de álbuns não pode conter álbuns duplicados.", "Corrija a lista de álbuns.");
                    }
                }
                _albums = value;
            }
        }

        public List<Musica> Musicas
        {
            get {return _musicas;}
            set{
                if(value == null){
                    throw new ArgumentNullException(nameof(value), "A lista de músicas não pode ser nula.");
                }
                foreach (Musica musica in value)
                {
                    if (_musicas.Contains(musica))
                    {
                        throw new ArtistaException("A lista de músicas não pode conter músicas duplicadas.", "Corrija a lista de músicas.");
                    }
                }
                _musicas = value;
            }
        }

        public Artista()
        {
            
        }

        public Artista(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
            _albums = new List<Album>();
            _musicas = new List<Musica>();
        }
    }
}