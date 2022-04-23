using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxCalc.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseNavigationPage : NavigationPage
    {
        public BaseNavigationPage(Page page) : base(page)
        {
            InitializeComponent();
        }

        public BaseNavigationPage()
        {
            InitializeComponent();
        }
    }
}