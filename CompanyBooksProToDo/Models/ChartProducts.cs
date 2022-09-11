namespace CompanyBooksProECom.Models
{
    public class ChartProduct
    {
        public long DocumentItemItemId { get; set; }
        public string DocumentItemItemName { get; set; }
        public string DocumentItemItemNumber { get; set; }
        public double DocumentItemPriceCurrency { get; set; }
        public double DocumentItemPriceCurrencyIncludeTaxes { get; set; }
        public double DocumentItemQuantity { get; set; }
        public double DocumentItemTotalCurrencyOne { get; set; }
        public double DocumentItemTotalCurrencyTwo { get; set; }
        public double DocumentItemValue { get; set; }
    }
}
