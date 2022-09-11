using CompanyBooksProToDo.Models.TODO;
using System.Collections.Generic;

namespace CompanyBooksProToDo.ViewModels.Home
{
    public class CreateItemVM
    {
        public Mission Mission { get; set; } = new Mission();
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}
