namespace TaxCalc.API.Interfaces
{
    public interface  IOrderTax
    {
        float order_total_amount { get; set; }
        float shipping { get; set; }
        float taxable_amount { get; set; }
        float amount_to_collect { get; set; }
        float rate { get; set; }
        bool has_nexus { get; set; }
        bool freight_taxable { get; set; }
        string tax_source { get; set; }
        string exemption_type { get; set; }
        object jurisdictions { get; set; }
        object breakdown { get; set; }
    }
}
