using System.Collections.Generic;

namespace TaskManager.Models.Pagination
{
    public class GenericPaginatedModel<TModel>
    {
        public IEnumerable<TModel> Models { get; set; }
        public int PageNumber { get; protected set; }
        public int Total { get; protected set; }
    }
}