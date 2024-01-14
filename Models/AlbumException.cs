using System;

namespace _DOTNET_PLAYLIST.Models
{
    public class AlbumException : Exception
    {
        public string SugestaoCorrecao { get; set; }

        public AlbumException(string message, string sugestao) : base(message)
        {
            SugestaoCorrecao = sugestao;
        }
    }
}
