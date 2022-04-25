namespace TaxCalc.API.Interfaces
{
    public interface ITaxRate
    {
        string zip { get; set; }
        string country { get; set; }
        string country_rate { get; set; }
        string state { get; set; }
        string state_rate { get; set; }
        string county { get; set; }
        string county_rate { get; set; }
        string city { get; set; }
        string city_rate { get; set; }
        string combined_district_rate { get; set; }
        string combined_rate { get; set; }
        bool freight_taxable { get; set; }
    }
}
