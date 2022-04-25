using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxCalc.Core.Views
{
    /// <summary>
    /// Base navigation page, useful for customizing navigation bar color.
    /// </summary>
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