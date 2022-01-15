using System;
using System.IO;
using test.data_access.evertec;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace test.espinosa_oscar.evertec
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.TipsListView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
