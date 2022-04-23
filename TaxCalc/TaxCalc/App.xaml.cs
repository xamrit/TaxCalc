using TaxCalc.Views;
using Xamarin.Forms;

namespace TaxCalc
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainFlyoutPage();
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