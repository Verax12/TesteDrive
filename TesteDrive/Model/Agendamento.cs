using System;
using System.Collections.Generic;
using System.Text;
using TesteDrive.ViewModels;
using Xamarin.Forms;

namespace TesteDrive.Model
{
    public class Agendamento : BaseViewModel
    {

        public string Nome
        {
            get { return Nome; }
            set
            {
                Nome = value;
            }
        }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public DateTime DataAgendamento { get { return DateTime.Today; } set { DataAgendamento = value; } }
        public TimeSpan HoraAgendamento { get; set; }
    }
}
