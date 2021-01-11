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
    public partial class AgendamentoPage : ContentPage
    {
        public AgendamentoViewModels Agendamento { get; set; }
        public AgendamentoPage(Veiculos veiculos)
        {

            InitializeComponent();
            Agendamento = new AgendamentoViewModels(veiculos);
            this.BindingContext = Agendamento;
            this.Title = "Agendamento do " + veiculos.nome;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Agendamento>(this, "AgendarCommand", (msg) =>
            {
                FinalizarAgendamento(msg);
            });
        }

        /// <summary>
        ///  POP UP DE CONFIRMAÇÃO ANTES DO ENVIO DOS FORMULARIOS
        /// </summary>
        /// <param name="msg"></param>
        private async void FinalizarAgendamento(Agendamento msg)
        {
            bool confirmar = await DisplayAlert("Confirmar Agendamento",
                 string.Format("Deseja realizar o agendamento no dia {0}?", msg.DataAgendamento), "Sim", "Não");

            if (confirmar)
            {
                Agendamento.SalvarAgendamento();

            }
            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento", async (msg2) =>
            {

                await DisplayAlert("Agendamento Confirmado",
                      string.Format("{0} Realizou um agendamento de um carro , para o dia {1}", msg.Nome, msg.DataAgendamento), "OK");
                await Navigation.PushAsync(new MainPage());

            });

            MessagingCenter.Subscribe<ArgumentException>(this, "FalhaAgendamento", async (msg3) =>
            {
                await DisplayAlert("Falha no Agendamento",
                    " Ocorreu um erro ao tentar salvar sua solicitação.\n Tente novamente mais tarde", "OK");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Agendamento>(this, "AgendarCommand");
            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FalhaAgendamento");
        }
    }
}