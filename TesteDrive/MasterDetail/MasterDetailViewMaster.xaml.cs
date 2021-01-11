using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TesteDrive.Media;
using TesteDrive.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteDrive.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailViewMaster : TabbedPage
    {
        public ListView ListView;

        public MasterDetailViewMaster(Usuario usuario)
        {
            InitializeComponent();
            BindingContext = new MasterDetailViewMasterViewModel(usuario);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Usuario>(this, "EditarPerfil",
                (msg) =>
                {
                    this.CurrentPage = this.Children[1];
                });

            MessagingCenter.Subscribe<Usuario>(this, "UsuarioSalvoComSucesso", (msg) =>
            {
                this.CurrentPage = this.Children[0];
            });

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Usuario>(this, "EditarPerfil");
            MessagingCenter.Unsubscribe<Usuario>(this, "UsuarioSalvoComSucesso");
        }

        #region ViewModel
        public class MasterDetailViewMasterViewModel : INotifyPropertyChanged
        {
            #region Parametros
            public MasterDetailViewMasterMenuItem MasterDetailView { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public string DataNascimento { get; set; }
            public bool EditarDadosPessoais { get; set; }
            public ImageSource FotoPerfil { get; set; }

            private readonly Usuario usuario;
            public ICommand PerfilCommand { get; private set; }
            public ICommand SalvarPerfilCommand { get; private set; }
            public ICommand HabilitaEditarUsuario { get; private set; }
            public ICommand TirarFotoCommand { get; private set; }
            #endregion

            public MasterDetailViewMasterViewModel(Usuario usuario)
            {
                this.FotoPerfil = "perfil.png";
                this.usuario = usuario;
                BindingData(usuario);
                Commandos();
            }

            private void Commandos()
            {
                PerfilCommand = new Command(
                                    () =>
                                    {
                                        MessagingCenter.Send<Usuario>(this.usuario, "EditarPerfil");
                                    });

                SalvarPerfilCommand = new Command(
                     () =>
                     {
                         ///Inserir chamada post para salvar o usuario na API

                        
                         ///Bloqueando o Botão salvar novamente
                         this.EditarDadosPessoais = false;
                         OnPropertyChanged("EditarDadosPessoais");
                         /// Redirecionar para a pagina Usuario novamente.
                         MessagingCenter.Send<Usuario>(this.usuario, "UsuarioSalvoComSucesso");
                     }
                     );

                HabilitaEditarUsuario = new Command(
                    () =>
                    {
                        this.EditarDadosPessoais = true;
                        OnPropertyChanged("EditarDadosPessoais");
                    });

                TirarFotoCommand = new Command(
                    () => 
                    {
                        DependencyService.Get<ICamera>().TirarFoto();
                    }
                    );

            }

            private void BindingData(Usuario usuario)
            {
                this.EditarDadosPessoais = false;
                this.Nome = usuario.nome;
                this.Email = usuario.email;
                this.Telefone = usuario.telefone;
                this.DataNascimento = usuario.datanascimento;
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
        #endregion
    }
}