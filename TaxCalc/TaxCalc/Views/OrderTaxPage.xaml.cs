using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxCalc.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderTaxPage : ContentPage
    {
        public OrderTaxPage()
        {
            InitializeComponent();
        }

        private async void CalculateOrderTaxButtonClicked(object sender, System.EventArgs e)
        {
            const string title = "Warning";
            const string description = "Please enter a valid Zip code.";
            const string accept = "OK";

            //if (string.IsNullOrWhiteSpace(ZipEntry.Text))
            //    await DisplayAlert(title, description, accept);
        }
    }
}