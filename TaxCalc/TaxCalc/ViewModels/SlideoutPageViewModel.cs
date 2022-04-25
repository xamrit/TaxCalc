using System.Collections.ObjectModel;
using TaxCalc.Core.Models;
using TaxCalc.Core.Views;

namespace TaxCalc.Core.ViewModels
{
    /// <summary>
    /// View model for the slide out menu.
    /// </summary>
    public class SlideoutPageViewModel : BaseViewModel
    {
        public ObservableCollection<FlyoutMenuItem> MenuItems { get; set; }

        /// <summary>
        /// Constructor. Define the slide out menu items.
        /// </summary>
        public SlideoutPageViewModel()
        {
            MenuItems = new ObservableCollection<FlyoutMenuItem>(new[]
            {
                    new FlyoutMenuItem { Id = 0, Title = "Location Rates", TargetType=typeof(TaxRatePage) },
                    new FlyoutMenuItem { Id = 1, Title = "Order Taxes", TargetType=typeof(OrderTaxPage) },
            });
        }
    }
}
