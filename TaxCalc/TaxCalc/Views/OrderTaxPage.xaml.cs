using TaxCalc.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxCalc.Core.Views
{
    /// <summary>
    /// The order tax page view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderTaxPage : ContentPage
    {
        public OrderTaxPage()
        {
            InitializeComponent();

            // Assign view model using App TaxService.
            BindingContext = new OrderTaxPageViewModel(App.TaxService);
        }
    }
}