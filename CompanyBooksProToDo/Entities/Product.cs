using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace CompanyBooksProECom.Entities
{
    public class Product
    {
        private double _priceWithoutDiscount;

        public long Id { get; set; }
        public string Number { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string SkuName { get; set; }
        public string MadeIn { get; set; }
        public double Price { get; set; }
        public int MinCount { get; set; }
        public double PriceWithoutDiscount
        {
            get => _priceWithoutDiscount;
            set => _priceWithoutDiscount = value == 0 ? Price + Price * .25 : value;
        }
        public double StockCount { get; set; }
        public string Description { get; set; }
        public string MoreInfo { get; set; }
        public string SupplyPeriod { get; set; }
        public string SupplyDate { get; set; }
        public string CategoryLevelName_0 { get; set; }
        public string CategoryLevelCaption_0 { get; set; }
        public string CategoryLevelName_1 { get; set; }
        public string CategoryLevelCaption_1 { get; set; }
        public string CategoryLevelName_2 { get; set; }
        public string CategoryLevelCaption_2 { get; set; }
        public string CategoryLevelName_3 { get; set; }
        public string CategoryLevelCaption_3 { get; set; }
        public string CategoryLevelName_4 { get; set; }
        public string CategoryLevelCaption_4 { get; set; }
        public string CategoryLevelName_5 { get; set; }
        public string CategoryLevelCaption_5 { get; set; }
        public string CategoryLevelName_6 { get; set; }
        public string CategoryLevelCaption_6 { get; set; }
        public string CategoryLevelName_7 { get; set; }
        public string CategoryLevelCaption_7 { get; set; }
        public double Row1Quantity { get; set; }
        public double Row1Price { get; set; }
        public double Row2Quantity { get; set; }
        public double Row2Price { get; set; }
        public double Row3Quantity { get; set; }
        public double Row3Price { get; set; }
        public long CompanyId { get; set; }
        public List<File> Files { get; set; }
    }
}
