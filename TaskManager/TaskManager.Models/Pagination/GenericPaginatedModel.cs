using System.Collections.Generic;

namespace TaskManager.Models.Pagination
{
    public class GenericPaginatedModel<TModel>
    {
        public IEnumerable<TModel> Models { get; set; }
        public PaginationModel PaginationModel {get; set;}
    }
}