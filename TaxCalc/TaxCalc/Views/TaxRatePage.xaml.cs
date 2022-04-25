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
    }
}