using System.Collections.Generic;

namespace TaskManager.Models.Pagination
{
    public class SelectionModel<T>
    {
        public int TotalCount {get; set;}
        public List<T> Result {get; set;}
    }
}