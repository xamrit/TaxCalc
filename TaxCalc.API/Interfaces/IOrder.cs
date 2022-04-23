namespace TaxCalc.API.Interfaces
{
    public interface IOrder
    {
        string from_country { get; set; }
        string from_zip { get; set; }
        string from_state { get; set; }
        string from_city { get; set; }
        string from_street { get; set; }
        string to_country { get; set; }
        string to_zip { get; set; }
        string to_state { get; set; }
        string to_city { get; set; }
        string to_street { get; set; }
        float amount { get; set; }
        float shipping { get; set; }
    }
}
