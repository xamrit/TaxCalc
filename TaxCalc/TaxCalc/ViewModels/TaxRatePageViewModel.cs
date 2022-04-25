using System.Text;
using System.Windows.Input;
using TaxCalc.Core.Models;
using TaxCalc.Core.Services;
using Xamarin.Forms;

namespace TaxCalc.Core.ViewModels
{
    /// <summary>
    /// View model for the tax rate page view.
    /// </summary>
    public class TaxRatePageViewModel : BaseViewModel
    {
        private const string NoResults = "No Results";
        private ITaxService _taxService;

        private string _taxRateResults;
        private string _taxLocationResults;

        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }

        public ICommand GetTaxRateButtonCommand { get; set; }


        /// <summary>
        /// The displayed tax rate text.
        /// </summary>
        public string TaxRateResults
        {
            get => _taxRateResults; 
            set
            {
                _taxRateResults = value;
                OnPropertyChanged(nameof(TaxRateResults));
            }
        }

        /// <summary>
        /// The displayed tax rate location text.
        /// </summary>
        public string TaxLocationResults
        {
            get => _taxLocationResults;
            set
            {
                _taxLocationResults = value;
                OnPropertyChanged(nameof(TaxLocationResults));
            }
        }


        /// <summary>
        /// Constructor. Initializes services and commands.
        /// </summary>
        public TaxRatePageViewModel(ITaxService taxService)
        {
            _taxService = taxService;

            GetTaxRateButtonCommand = new Command(OnGetTaxRateButtonCommand);
        }


        /// <summary>
        /// Handles what occurs on tap of the "Get Tax Rates" button.
        /// Validates input. retrieves resulting <see cref="TaxRate"/> data and updates display. 
        /// If error occurs, "No Results" is displayed.
        /// </summary>
        private async void OnGetTaxRateButtonCommand(object obj)
        {
            const string title = "Warning";
            const string description = "Please enter a valid Zip code.";
            const string accept = "OK";
            TaxRate taxRate;

            // Validate input. At least zip code is needed to be entered.
            if (string.IsNullOrWhiteSpace(Zip))
            {
                await Application.Current?.MainPage?.DisplayAlert(title, description, accept);
                return;
            }

            // Try to get the location tax rates.
            try
            {
                taxRate = await _taxService?.GetLocationTaxRates(Zip, Country, State, City, Street);
            }
            catch
            {
                // If unable to get rates, update text to show as "No Results".
                TaxLocationResults = NoResults;
                TaxRateResults = string.Empty;
                return;
            }

            // Update the display text to show the rates.
            if (taxRate != null)
            {
                var builder = new StringBuilder();
                builder.AppendLine();
                builder.AppendLine("LOCATION");
                builder.AppendLine();
                builder.AppendLine($"Zip: {taxRate.zip}");
                builder.AppendLine($"Country: {taxRate.country}");
                builder.AppendLine($"State: {taxRate.state}");
                builder.AppendLine($"County: {taxRate.county}");
                builder.AppendLine($"City: {taxRate.city}");

                TaxLocationResults = builder.ToString();

                builder.Clear();

                builder.AppendLine();
                builder.AppendLine("RATES");
                builder.AppendLine();
                builder.AppendLine($"Country Rate: {taxRate.country_rate}");
                builder.AppendLine($"State Rate: {taxRate.state_rate}");
                builder.AppendLine($"County Rate: {taxRate.county_rate}");
                builder.AppendLine($"City Rate: {taxRate.city_rate}");
                builder.AppendLine($"Combined District Rate: {taxRate.combined_district_rate}");
                builder.AppendLine($"Combined Rate: {taxRate.combined_rate}");
                builder.AppendLine($"Freight Taxable: {taxRate.freight_taxable}");

                TaxRateResults = builder.ToString();
            }
            else
            {
                TaxLocationResults = NoResults;
                TaxRateResults = string.Empty;
            }
        }
    }
}
