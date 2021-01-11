using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDrive.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string datanascimento { get; set; }
        public string telefone { get; set; }
    }

    public class UsuarioLogin
    {
        public Usuario usuario { get; set; }
    }
}
