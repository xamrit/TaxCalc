using System.Collections.ObjectModel;
using TaxCalc.Core.Models;
using TaxCalc.Core.Views;

namespace TaxCalc.Core.ViewModels
{
    public class SlideoutPageViewModel : BaseViewModel
    {
        public ObservableCollection<FlyoutMenuItem> MenuItems { get; set; }

        public SlideoutPageViewModel()
        {
            MenuItems = new ObservableCollection<FlyoutMenuItem>(new[]
            {
                    new FlyoutMenuItem { Id = 0, Title = "Location Rate", TargetType=typeof(TaxRatePage) },
                    new FlyoutMenuItem { Id = 1, Title = "Order Tax", TargetType=typeof(OrderTaxPage) },
            });
        }
    }
}
