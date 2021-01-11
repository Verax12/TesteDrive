using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDrive.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteDrive.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailView : MasterDetailPage
    {
        private readonly Usuario usuario;
        public MasterDetailView(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.Master = new MasterDetailViewMaster(this.usuario);

            //MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }
    }
}