using System;
using TesteDrive.MasterDetail;
using TesteDrive.Model;
using TesteDrive.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteDrive
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
            //#if DEBUG
            //            MainPage = new NavigationPage(new MainPage());
            //#endif
        }

        protected override void OnStart()
        {
            MessagingCenter.Subscribe<Usuario>(this, "SucessoLogin",
           (usuario) =>
            {
                //MainPage = new NavigationPage(new MainPage());
                MainPage = new MasterDetailView(usuario);
            });
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
