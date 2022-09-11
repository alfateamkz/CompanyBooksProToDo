using CompanyBooksProToDo.Models.TODO;
using System;
using System.Collections.Generic;

namespace CompanyBooksProToDo.ViewModels.Home
{
    public class IndexVM
    {
        public bool FilteredByOpenedTasks { get; set; }
        public bool FilteredByClosedTasks { get; set; }
        public string TextFilter { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;


        public List<Mission> Missions { get; set; } = new List<Mission>();

    }
}
