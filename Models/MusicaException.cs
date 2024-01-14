using System;

namespace _DOTNET_PLAYLIST.Models
{
    public class MusicaException : Exception
    {
        public string SugestaoCorrecao { get; set; }

        public MusicaException(string message, string sugestao) : base(message)
        {
            SugestaoCorrecao = sugestao;
        }
    }
}
