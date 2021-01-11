using TesteDrive.Model;
using TesteDrive.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesPage : ContentPage
    {

        public Veiculos Veiculos { get; set; }
        public DetalhesVeiculosViewModel Detalhes { get; set; }
        public DetalhesPage(Veiculos veiculos)
        {
            InitializeComponent();
            this.Veiculos = veiculos;
            Detalhes = new DetalhesVeiculosViewModel(Veiculos);
            this.BindingContext = Detalhes;
        }

        //USANDO EVENTO CLICKED 
        //private void btnProximo_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new AgendamentoPage(Veiculos));
        //}


        //Usando mensageria
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Veiculos>(this, "ProximoCommand", (msg) =>
            {
                Navigation.PushAsync(new AgendamentoPage(msg));
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Veiculos>(this, "ProximoCommand");
        }
    }
}