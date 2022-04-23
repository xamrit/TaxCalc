namespace TaxCalc.API.Interfaces
{
    public interface ITaxRate
    {
        string zip { get; set; }
        string country { get; set; }
        float country_rate { get; set; }
        string state { get; set; }
        float state_rate { get; set; }
        string county { get; set; }
        float county_rate { get; set; }
        string city { get; set; }
        float city_rate { get; set; }
        float combined_district_rate { get; set; }
        float combined_rate { get; set; }
        float freight_taxable { get; set; }
    }
}
