using TaxCalc.Core.Services;
using TaxCalc.Core.Views;
using Xamarin.Forms;

namespace TaxCalc.Core
{
    public partial class App : Application
    {
        public static ITaxService TaxService;

        public App()
        {
            InitializeComponent();

            TaxService = new TaxService();
            TaxService.Initialize();

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