using TesteDrive.Model;
using TesteDrive.ViewModels;
using TesteDrive.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TesteDrive
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public ListagemViewModel ListagemViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            this.ListagemViewModel = new ListagemViewModel();
            this.BindingContext = this.ListagemViewModel;
        }

        /// <summary>
        /// Usando o componente ItemTapped
        /// </summary>
        //private void listViewVeiculos_itemtapped(object sender, ItemTappedEventArgs e)
        //{
        //    Veiculos veiculo = (Veiculos)e.Item;

        //    Navigation.PushAsync(new DetalhesPage(veiculo));

        //    ///Usado para exibir um alerda na tela
        //    //DisplayAlert("Titulo", "Mensagem", "Mensagem no botão de ok");
        //}


        /// Usando Mensageria

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Veiculos>(this, "VeiculoSelecionado",
                (msg) =>
                {
                    Navigation.PushAsync(new DetalhesPage(msg));
                });
            await ListagemViewModel.GetVeiculosAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Veiculos>(this, "VeiculoSelecionado");

        }

    }
}

