using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TesteDrive.Model;
using Xamarin.Forms;

namespace TesteDrive.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// login: joao@alura.com.br
        /// senha: alura123
        /// </summary>
        private string usuario;

        public string Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                // Convertendo a Propriedade de ICommand para Command
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        private string senha;

        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
                // Convertendo a Propriedade de ICommand para Command
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        public ICommand EntrarCommand { get; private set; }


        public LoginViewModel()
        {
            EntrarCommand = new Command(
               // Comando Mensageria
               async () =>
               {
                   await LoginAutenticacao();
               },
                // Validação
                () =>
                {
                    return !string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(senha);
                }
            );
        }

        private async Task LoginAutenticacao()
        {

            using (var cliente = new HttpClient())
            {
                FormUrlEncodedContent camposFormulario = new FormUrlEncodedContent(
                    new[] {
                            new KeyValuePair<string, string>("email", usuario),
                            new KeyValuePair<string, string>("senha", senha)
                    });
                cliente.BaseAddress = new Uri("https://aluracar.herokuapp.com");

                HttpResponseMessage result = null;

                try
                {
                    result = await cliente.PostAsync("/login", camposFormulario);
                }

                catch
                {

                    MessagingCenter.Send<LoginException>(new LoginException("@Ocorreu um erro de comunicação com o Servidor." +
                        "Favor verificar sua conexão de Internet tentar novamente"), "FalhaLogin");
                }

                if (result.IsSuccessStatusCode)
                {
                    var resultado_login = JsonConvert.DeserializeObject<UsuarioLogin>(await result.Content.ReadAsStringAsync());
                    MessagingCenter.Send<Usuario>(resultado_login.usuario, "SucessoLogin");
                }
                else
                    MessagingCenter.Send<LoginException>(new LoginException("Usuario ou Senha Invalidos"), "FalhaLogin");
            };

        }
    }
    partial class LoginException : Exception
    {
        public LoginException(string mensagem) : base(mensagem)
        {

        }
    }
}
