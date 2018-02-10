namespace Eticadata.Cust.WebServices.Models
{
    public class myItem
    {
        public myItem() { }

        public string Code { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public string Abbreviation { get; set; } = "";
        public int VATRateSale { get; set; } = 0;
        public int VATRatePurchase { get; set; } = 0;
        public string MeasureOfStock { get; set; } = "";
        public string MeasureOfSale { get; set; } = "";
        public string MeasureOfPurchase { get; set; } = "";
    }
}