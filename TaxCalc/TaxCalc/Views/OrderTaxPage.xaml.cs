using TaxCalc.Core.ViewModels;
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

            BindingContext = new OrderTaxPageViewModel(App.TaxService);
        }
    }
}