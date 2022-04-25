using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaxCalc.Core.ViewModels
{
    /// <summary>
    /// The base view model intended to be used by all view models. 
    /// Provides <see cref="INotifyPropertyChanged"/> implementation.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
