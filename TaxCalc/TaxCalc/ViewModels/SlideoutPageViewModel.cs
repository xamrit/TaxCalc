using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaxCalc.Models;
using TaxCalc.Views;

namespace TaxCalc.ViewModels
{
    public class SlideoutPageViewModel : INotifyPropertyChanged
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

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
