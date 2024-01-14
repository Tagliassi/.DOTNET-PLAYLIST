using System;

namespace _DOTNET_PLAYLIST.Models
{
    public class ArtistaException : Exception
    {
        public string SugestaoCorrecao { get; set; }

        public ArtistaException(string message, string sugestao) : base(message)
        {
            SugestaoCorrecao = sugestao;
        }
    }
}
