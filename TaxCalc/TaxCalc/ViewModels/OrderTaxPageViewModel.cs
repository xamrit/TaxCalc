using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxCalc.Core.Models;
using TaxCalc.Core.Services;
using Xamarin.Forms;

namespace TaxCalc.Core.ViewModels
{
    /// <summary>
    /// View model for the order tax page view.
    /// </summary>
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

        /// <summary>
        /// The displayed tax text.
        /// </summary>
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

        /// <summary>
        /// Constructor. Initializes services and commands.
        /// </summary>
        public OrderTaxPageViewModel(ITaxService taxService)
        {
            _taxService = taxService;

            GetOrderTaxButtonCommand = new Command(OnGetOrderTaxButtonCommand);
        }

        /// <summary>
        /// Shows dialog if input is missing or invalid.
        /// </summary>
        /// <returns>Returns false if invalid input, true otherwise.</returns>
        private async Task<bool> IsValidInput()
        {
            const string title = "Warning";
            const string accept = "OK";

            // Build description message for every invalid input.
            var descriptionBuilder = new StringBuilder();

            if (!double.TryParse(Amount, out _))
                descriptionBuilder.AppendLine("Invalid Amount.");
            if (!double.TryParse(Shipping, out _))
                descriptionBuilder.AppendLine("Invalid Shipping.");
            if (string.IsNullOrWhiteSpace(ToCountry))
                descriptionBuilder.AppendLine("Invalid To Country.");
            if ((ToCountry?.ToLowerInvariant() == "us" || ToCountry?.ToLowerInvariant() == "ca") && string.IsNullOrWhiteSpace(ToState))
                descriptionBuilder.AppendLine("Invalid To State.");
            if (ToCountry?.ToLowerInvariant() == "us" && string.IsNullOrWhiteSpace(ToZip))
                descriptionBuilder.AppendLine("Invalid To Zip.");

            // If description message has any content, then there is invalid input. Show the warning dialog.
            if (descriptionBuilder.Length > 0)
            {
                await Application.Current?.MainPage?.DisplayAlert(title, descriptionBuilder.ToString(), accept);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles what occurs on tap of the "Calculate Order Tax" button.
        /// Validates input. Sends <see cref="Order"/> data and retrieves 
        /// resulting <see cref="OrderTax"/> data and updates display. If
        /// error occurs, "No Results" is displayed.
        /// </summary>
        private async void OnGetOrderTaxButtonCommand(object obj)
        {
            OrderTax tax;

            // Validate the inputs.
            if (!await IsValidInput())
                return;

            // Create a new order obect to send.
            var order = new Order()
            {
                from_country = string.IsNullOrEmpty(FromCountry) ? null : FromCountry,
                from_zip = string.IsNullOrEmpty(FromZip) ? null : FromZip,
                from_state = string.IsNullOrEmpty(FromState) ? null : FromState,
                from_city = string.IsNullOrEmpty(FromCity) ? null : FromCity,
                from_street = string.IsNullOrEmpty(FromStreet) ? null : FromStreet,
                to_country = string.IsNullOrEmpty(ToCountry) ? null : ToCountry,
                to_zip = string.IsNullOrEmpty(ToZip) ? null : ToZip,
                to_state = string.IsNullOrEmpty(ToState) ? null : ToState,
                to_city = string.IsNullOrEmpty(ToCity) ? null : ToCity,
                to_street = string.IsNullOrEmpty(ToStreet) ? null : ToStreet,
                amount = float.TryParse(Amount, out var fAmount) ? fAmount : 0,
                shipping = float.TryParse(Shipping, out var fShipping) ? fShipping : 0,
            };

            // Try to get the tax for the order.
            try
            {
                tax = await _taxService?.GetTaxForOrder(order);
            }
            catch
            {
                // If unsuccessful, update message to read "No Results".
                OrderTaxResults = NoResults;
                return;
            }

            if (tax != null)
            {
                // Update the display to show the tax.

                var builder = new StringBuilder();

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
