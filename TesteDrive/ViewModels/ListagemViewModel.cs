using TesteDrive.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace TesteDrive.ViewModels
{
    public class ListagemViewModel : BaseViewModel
    {
        public ObservableCollection<Veiculos> Veiculos { get; set; }
        const string URL = "http://aluracar.herokuapp.com/";

        private bool aguarde;

        public bool Aguarde
        {
            get { return aguarde; }
            set
            {
                aguarde = value;
                OnPropertyChanged();
            }
        }

        Veiculos veiculosSelecionado;

        public Veiculos VeiculosSelecionado
        {
            get
            {
                return veiculosSelecionado;
            }
            set
            {
                veiculosSelecionado = value;
                if (value != null)
                    MessagingCenter.Send(veiculosSelecionado, "VeiculoSelecionado");
            }
        }

        public ListagemViewModel()
        {
            this.Veiculos = new ObservableCollection<Veiculos>();
        }

        public async Task GetVeiculosAsync()
        {
            Aguarde = true;
            HttpClient httpClient = new HttpClient();
            var resultado = await httpClient.GetStringAsync(URL);

            Veiculos[] veiculos = JsonConvert.DeserializeObject<Veiculos[]>(resultado);

            foreach (var item in veiculos)
            {
                Veiculos.Add(item);
            }
            Aguarde = false;
        }
    }
}
