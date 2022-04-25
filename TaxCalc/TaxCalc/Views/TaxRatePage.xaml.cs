using TaxCalc.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxCalc.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxRatePage : ContentPage
    {
        public TaxRatePage()
        {
            InitializeComponent();

            BindingContext = new TaxRatePageViewModel(App.TaxService);
        }

        private async void GetTaxRateButtonClicked(object sender, System.EventArgs e)
        {
            const string title = "Warning";
            const string description = "Please enter a valid Zip code.";
            const string accept = "OK";

            if (string.IsNullOrWhiteSpace(ZipEntry.Text))
                await DisplayAlert(title, description, accept);
        }
    }
}