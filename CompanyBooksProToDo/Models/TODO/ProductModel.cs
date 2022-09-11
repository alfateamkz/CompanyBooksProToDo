namespace CompanyBooksProToDo.Models.TODO
{
    public class ProductModel
    {
        public int ID_ITEM { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_NUMBER { get; set; }

        public override string ToString()
        {
            return ITEM_NAME;
        }
    }
}
