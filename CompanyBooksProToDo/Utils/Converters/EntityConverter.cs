using System.Collections.Generic;
using MBM.DB;
using System.Data;
using System.Linq;
using CompanyBooksProECom.Entities;
using CompanyBooksProECom.Models;

namespace CompanyBooksProECom.Utils.Converters
{
    public static class EntityConverter
    {
        public static List<Product> ToProductEntity(this IEnumerable<DataRow> dataRows) =>
            dataRows.Select(dataRow => new Product
                {
                    Id = dataRow.Field<long>(MBM_ITEMS.ID_ITEM),
                    Number = dataRow.Field<string>(MBM_ITEMS.ITEM_NUMBER),
                    Category = dataRow.Field<string>(MBM_ITEMS.ITEM_SUBTYPE_NAME),
                    Name = dataRow.Field<string>(MBM_ITEMS.ITEM_NAME),
                    SkuName = dataRow.Field<string>(MBM_ITEMS.ITEM_ALTERNATIVE),
                    MadeIn = dataRow.Field<string>(MBM_ITEMS.ITEM_MADEIN),
                    Price = dataRow.Field<double>(MBM_ITEMS.ITEM_SALE_PRICE_1_INCLUDE_TAX_CURRENCY_1),
                    PriceWithoutDiscount = dataRow.Field<double>(MBM_ITEMS.ITEM_SALE_PRICE_4_INCLUDE_TAX_CURRENCY_1),
                    StockCount = dataRow.Field<double>(MBM_ITEMS.ITEM_TOTAL_STOCK),
                    Description = dataRow.Field<string>(MBM_ITEMS.ITEM_DESCRIPTION_1),
                    MoreInfo = dataRow.Field<string>(MBM_ITEMS.ITEM_DESCRIPTION_2),
                    SupplyPeriod = dataRow.Field<string>(MBM_ITEMS.ITEM_CHARACTERISTIC_2),
                    SupplyDate = dataRow.Field<string>(MBM_ITEMS.ITEM_CHARACTERISTIC_3),
                    CategoryLevelCaption_0 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_0_CAPTION),
                    CategoryLevelCaption_1 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_1_CAPTION),
                    CategoryLevelCaption_2 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_2_CAPTION),
                    CategoryLevelCaption_3 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_3_CAPTION),
                    CategoryLevelCaption_4 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_4_CAPTION),
                    CategoryLevelCaption_5 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_5_CAPTION),
                    CategoryLevelCaption_6 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_6_CAPTION),
                    CategoryLevelCaption_7 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_7_CAPTION),
                    CategoryLevelName_0 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_0_NAME),
                    CategoryLevelName_1 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_1_NAME),
                    CategoryLevelName_2 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_2_NAME),
                    CategoryLevelName_3 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_3_NAME),
                    CategoryLevelName_4 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_4_NAME),
                    CategoryLevelName_5 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_5_NAME),
                    CategoryLevelName_6 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_6_NAME),
                    CategoryLevelName_7 = dataRow.Field<string>(MBM_ITEMS.ITEM_CATEGORY_LEVEL_7_NAME),
                    MinCount = int.TryParse(dataRow.Field<string>(MBM_ITEMS.ITEM_CHARACTERISTIC_4), out var count) ? count : 1,
                    Row1Price = dataRow.Field<double>(MBM_ITEMS.ITEM_FORMULA_PART_1),
                    Row2Price = dataRow.Field<double>(MBM_ITEMS.ITEM_FORMULA_PART_2),
                    Row3Price = dataRow.Field<double>(MBM_ITEMS.ITEM_FORMULA_PART_3),
                    Row1Quantity = dataRow.Field<double>(MBM_ITEMS.ITEM_FORMULA_PART_6),
                    Row2Quantity = dataRow.Field<double>(MBM_ITEMS.ITEM_FORMULA_PART_7),
                    Row3Quantity = dataRow.Field<double>(MBM_ITEMS.ITEM_FORMULA_PART_8),
                    CompanyId = dataRow.Field<long>(MBM_ITEMS.COMPANY_ID),
                    Files = new List<File>()
                })
                .ToList();

        public static List<Category> ToCategoryEntity(this IEnumerable<DataRow> dataRows) =>
            dataRows.Select(dataRow => new Category
            {
                Name = dataRow.Field<string>(MBM_ITEMS.ITEM_SUBTYPE_NAME)
            }).ToList();

        public static List<File> ToFileEntity(this IEnumerable<DataRow> dataRows) =>
            dataRows.Select(dataRow => new File
            {
                Id = dataRow.Field<long>(MBM_FILES_ATTACHED.ID_FILE_ATTACHED),
                ParentId = dataRow.Field<long>(MBM_FILES_ATTACHED.FILE_ATTACHED_TO_ID), 
                Url = dataRow.Field<string>(MBM_FILES_ATTACHED.FILE_ATTACHED_RELATIVE_PATH)
            }).ToList();

        public static List<User> ToUserEntity(this IEnumerable<DataRow> dataRows) =>
            dataRows.Select(dataRow => new User
            {
                Id = dataRow.Field<long>(MBM_CLIENTS.ID_CLIENT),
                Name = dataRow.Field<string>(MBM_CLIENTS.CLIENT_NAME),
                SubtypeNumber = dataRow.Field<long>(MBM_CLIENTS.CLIENT_SUBTYPE_NUMBER).ToString(),
                SubtypeName = dataRow.Field<string>(MBM_CLIENTS.CLIENT_SUBTYPE_NAME),
                CompanyName = dataRow.Field<string>(MBM_CLIENTS.CLIENT_NAME_COMPANY_NAME),
                WebSite = dataRow.Field<string>(MBM_CLIENTS.CLIENT_WEBSITE_1),
                AccreditedBusiness = dataRow.Field<string>(MBM_CLIENTS.CLIENT_ACCREDITED_BUSINESS),
                SecondName = dataRow.Field<string>(MBM_CLIENTS.CLIENT_NAME_SECOND_NAME),
                FirstName = dataRow.Field<string>(MBM_CLIENTS.CLIENT_NAME_FIRST_NAME),
                IdentityNumber = dataRow.Field<string>(MBM_CLIENTS.CLIENT_IDENTITY_NUMBER),
                PB = dataRow.Field<string>(MBM_CLIENTS.CLIENT_ADDRESS_PB_1),
                Country = dataRow.Field<string>(MBM_CLIENTS.CLIENT_ADDRESS_COUNTRY_1),
                State = dataRow.Field<string>(MBM_CLIENTS.CLIENT_ADDRESS_STATE_1),
                City = dataRow.Field<string>(MBM_CLIENTS.CLIENT_ADDRESS_CITY_1),
                District = dataRow.Field<string>(MBM_CLIENTS.CLIENT_ADDRESS_CITY_DISTRICT_1),
                Street = dataRow.Field<string>(MBM_CLIENTS.CLIENT_ADDRESS_STREET_1),
                House = dataRow.Field<string>(MBM_CLIENTS.CLIENT_ADDRESS_HOUSE_1),
                Apartament = dataRow.Field<string>(MBM_CLIENTS.CLIENT_ADDRESS_APARTMENT_1),
                ZIP = dataRow.Field<string>(MBM_CLIENTS.CLIENT_ADDRESS_ZIP_1),
                Phone_1 = dataRow.Field<string>(MBM_CLIENTS.CLIENT_PHONE_1),
                Phone_2 = dataRow.Field<string>(MBM_CLIENTS.CLIENT_PHONE_2),
                PermissionName = dataRow.Field<string>(MBM_CLIENTS.CLIENT_PERMISSION_USER_NAME),
                Password = dataRow.Field<string>(MBM_CLIENTS.CLIENT_PERMISSION_PASSWORD),
                Email_1 = dataRow.Field<string>(MBM_CLIENTS.CLIENT_EMAIL_1),
                Email_2 = dataRow.Field<string>(MBM_CLIENTS.CLIENT_EMAIL_2),
            }).ToList();
    }
}
