using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxCalc.Core.Models;
using TaxCalc.Core.Services;
using Xamarin.Forms;

namespace TaxCalc.Core.ViewModels
{
    public class OrderTaxPageViewModel : BaseViewModel
    {
        private ITaxService _taxService;
        private string _orderTaxResults;
        private const string NoResults = "No Results";


        public string Amount { get; set; }
        public string Shipping { get; set; }

        public string FromCountry { get; set; }
        public string FromState { get; set; }
        public string FromCity { get; set; }
        public string FromStreet { get; set; }
        public string FromZip { get; set; }


        public string ToCountry { get; set; }
        public string ToState { get; set; }
        public string ToCity { get; set; }
        public string ToStreet { get; set; }
        public string ToZip { get; set; }

        public string OrderTaxResults
        {
            get => _orderTaxResults;
            set
            {
                _orderTaxResults = value;
                OnPropertyChanged(nameof(OrderTaxResults));
            }
        }

        public ICommand GetOrderTaxButtonCommand { get; set; }

        public OrderTaxPageViewModel(Services.ITaxService taxService)
        {
            _taxService = taxService;

            GetOrderTaxButtonCommand = new Command(OnGetOrderTaxButtonCommand);
        }

        private async Task<bool> ValidateInputs()
        {
            const string title = "Warning";
            const string description = "Please enter a valid Zip code.";
            const string accept = "OK";
            await Application.Current.MainPage.DisplayAlert(title, description, accept);
            return false;
        }


        private async void OnGetOrderTaxButtonCommand(object obj)
        {
            OrderTax tax;
            var builder = new StringBuilder();

            //ValidateInputs();

            var order = new Order()
            {
                from_country = FromCountry,
                from_zip = FromZip,
                from_state = FromState,
                from_city = FromCity,
                from_street = FromStreet,
                to_country = ToCountry,
                to_zip = ToZip,
                to_state = ToState,
                to_city = ToCity,
                to_street = ToStreet,
                amount = float.TryParse(Amount, out var fAmount) ? fAmount : 0,
                shipping = float.TryParse(Shipping, out var fShipping) ? fShipping : 0,
            };

            try
            {
                tax = await _taxService?.GetTaxForOrder(order);
            }
            catch
            {
                OrderTaxResults = NoResults;
                return;
            }

            if (tax != null)
            {
                builder.Clear();

                var hasNexus = tax.has_nexus ? "Yes" : "No";
                var freightTaxable = tax.freight_taxable ? "Yes" : "No";

                builder.AppendLine($"Order Total Amount: {tax.order_total_amount:C}");
                builder.AppendLine($"Shipping: {tax.shipping:C}");
                builder.AppendLine($"Taxible Amount: {tax.taxable_amount:C}");
                builder.AppendLine($"Amount to collect: {tax.amount_to_collect:C}");
                builder.AppendLine($"Rate: {tax.rate:C}");
                builder.AppendLine($"Tax Source: {tax.tax_source}");
                builder.AppendLine($"Exemption Type: {tax.exemption_type}");
                builder.AppendLine($"Has Nexus?: {hasNexus}");
                builder.AppendLine($"Freight Taxible?: {freightTaxable}");

                OrderTaxResults = builder.ToString();
            }
            else
            {
                OrderTaxResults = NoResults;
            }
        }
    }
}
