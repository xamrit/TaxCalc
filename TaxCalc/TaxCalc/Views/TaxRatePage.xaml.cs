using TaxCalc.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxCalc.Core.Views
{
    /// <summary>
    /// The tax rate page view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxRatePage : ContentPage
    {
        public TaxRatePage()
        {
            InitializeComponent();

            // Assign view model using App TaxService.
            BindingContext = new TaxRatePageViewModel(App.TaxService);
        }
    }
}