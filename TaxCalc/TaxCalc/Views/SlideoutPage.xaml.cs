using TaxCalc.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxCalc.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SlideoutPage : ContentPage
    {
        public ListView ListView;

        public SlideoutPage()
        {
            InitializeComponent();

            BindingContext = new SlideoutPageViewModel();
            ListView = MenuItemsListView;
        }
    }
}