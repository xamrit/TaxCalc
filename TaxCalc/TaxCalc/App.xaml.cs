using TaxCalc.Core.Services;
using TaxCalc.Core.Views;
using Xamarin.Forms;

namespace TaxCalc.Core
{
    /// <summary>
    /// The Xamarin Forms <see cref="Application"/> implementation.
    /// Initializes the app's <see cref="ITaxService"/> implementation.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The application <see cref="ITaxService"/> implementation.
        /// </summary>
        public static ITaxService TaxService { get; set; }

        /// <summary>
        /// Initializes the main app flyout page and services.
        /// </summary>
        public App()
        {
            InitializeComponent();

            TaxService = new TaxService();

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