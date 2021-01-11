using TesteDrive.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TesteDrive.ViewModels
{
    public class DetalhesVeiculosViewModel : BaseViewModel
    {
        public Veiculos Veiculos { get; set; }
        public string FreioAbs { get { return string.Format("FreioABS -  R$ {0}", Veiculos.FREIO_ABS); } }
        public string ArCondicionado { get { return string.Format("Ar Condicionado -  R$ {0}", Veiculos.ARCONDICIONADO); } }
        public string Mp3 { get { return string.Format("Mp3 -  R$ {0}", Veiculos.MP3); } }

        public bool TemFreioAbs
        {
            get { return Veiculos.TemFreioAbs; }
            set
            {
                Veiculos.TemFreioAbs = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(TextValorTotal));

                //if (Veiculos.TemFreioAbs)
                //    DisplayAlert("Freios ABS", "FreioAbs ligado com sucesso", "Ok");
                //else
                //    DisplayAlert("Freios ABS", "Freios Desligados", "OK");

            }
        }

        public bool TemArCondicionado
        {
            get { return Veiculos.TemArCondicionado; }
            set
            {
                Veiculos.TemArCondicionado = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TextValorTotal));
            }
        }

        public bool TemMP3
        {
            get
            { return Veiculos.TemMp3; }
            set
            {
                Veiculos.TemMp3 = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TextValorTotal));
            }
        }

        public string TextValorTotal
        {
            get
            {
                return string.Format("Valor Total {0}", Veiculos.preco +
                    (TemFreioAbs ? Veiculos.FREIO_ABS : 0) +
                    (TemArCondicionado ? Veiculos.ARCONDICIONADO : 0) +
                    (TemMP3 ? Veiculos.MP3 : 0));
            }
        }

        public DetalhesVeiculosViewModel(Veiculos veiculos)
        {
            this.Veiculos = veiculos;
            ProximoCommand = new Command(
                () =>
                {
                    MessagingCenter.Send(Veiculos, "ProximoCommand");
                });
        }
     
        public ICommand ProximoCommand { get; set; }
    }
}
