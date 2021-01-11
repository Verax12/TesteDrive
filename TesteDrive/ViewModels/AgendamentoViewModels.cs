using TesteDrive.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TesteDrive.ViewModels
{
    public class AgendamentoViewModels : BaseViewModel
    {
        const string URL_POST = "http://aluracar.herokuapp.com/salvaragendamento";
        public Veiculos Veiculos { get; set; }
        public Agendamento agendamento { get; set; }
        public AgendamentoViewModels(Veiculos veiculos)
        {
            agendamento = new Agendamento();
            Veiculos = veiculos;
            AgendarCommand = new Command(() =>
            {
                MessagingCenter.Send<Agendamento>(agendamento, "AgendarCommand");
            });
        }


        public async void SalvarAgendamento()
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(new
            {
                nome = agendamento.Nome,
                fone = agendamento.Telefone,
                email = agendamento.Email,
                carro = Veiculos.nome,
                preco = Veiculos.preco,
                dataAgendamento = new DateTime(agendamento.DataAgendamento.Year, agendamento.DataAgendamento.Month, agendamento.DataAgendamento.Day, agendamento.HoraAgendamento.Hours, agendamento.HoraAgendamento.Minutes, agendamento.HoraAgendamento.Seconds)
            });

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            var resposta = await client.PostAsync(URL_POST, conteudo);
            if (resposta.IsSuccessStatusCode)
            {
                MessagingCenter.Send<Agendamento>(this.agendamento, "SucessoAgendamento");
            }
            else
            {
                MessagingCenter.Send<ArgumentException>(new ArgumentException(), "FalhaAgendamento");
            }
        }
        public ICommand AgendarCommand { get; set; }
    }
}
