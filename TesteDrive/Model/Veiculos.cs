using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDrive.Model
{
    public class Veiculos
    {
        public string nome { get; set; }
        public int preco { get; set; }
        public string PrecoFormatado { get { return string.Format("R$ " + preco.ToString()); } }
        public string Descricao { get; set; }

        public const int FREIO_ABS = 800;
        public const int ARCONDICIONADO = 1000;
        public const int MP3 = 500;

        public bool TemFreioAbs { get; set; }
        public bool TemArCondicionado { get; set; }
        public bool TemMp3 { get; set; }

    }
}
