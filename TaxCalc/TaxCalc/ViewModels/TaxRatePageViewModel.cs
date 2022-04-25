using System.Text;
using System.Windows.Input;
using TaxCalc.Core.Models;
using TaxCalc.Core.Services;
using Xamarin.Forms;

namespace TaxCalc.Core.ViewModels
{
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

        public string TaxRateResults
        {
            get => _taxRateResults; 
            set
            {
                _taxRateResults = value;
                OnPropertyChanged(nameof(TaxRateResults));
            }
        }
        public string TaxLocationResults
        {
            get => _taxLocationResults;
            set
            {
                _taxLocationResults = value;
                OnPropertyChanged(nameof(TaxLocationResults));
            }
        }

        public TaxRatePageViewModel(ITaxService taxService)
        {
            _taxService = taxService;

            GetTaxRateButtonCommand = new Command(OnGetTaxRateButtonCommand);
        }

        private async void OnGetTaxRateButtonCommand(object obj)
        {
            TaxRate taxRate;

            if (string.IsNullOrWhiteSpace(Zip))
                return;

            try
            {
                taxRate = await _taxService?.GetLocationTaxRatesForZipCode(Zip, Country, State, City, Street);
            }
            catch
            {
                TaxLocationResults = NoResults;
                TaxRateResults = string.Empty;
                return;
            }

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
        }
    }
}
