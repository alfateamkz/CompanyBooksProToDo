namespace CompanyBooksProECom.Entities
{
    public class File
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public string Url { get; set; }
        public string DefaultEmptyUrl { get; set; }
    }
}
