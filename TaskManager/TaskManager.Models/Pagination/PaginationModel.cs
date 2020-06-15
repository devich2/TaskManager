using System;

namespace TaskManager.Models.Pagination
{
    public class PaginationModel
    {
        public int PageNumber { get; protected set; }
        public int Total { get; protected set; }
        public PaginationModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            Total = (int)Math.Ceiling(count/(double)pageSize);
        }
    }
}