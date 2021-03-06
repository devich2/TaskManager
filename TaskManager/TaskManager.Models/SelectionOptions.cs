﻿using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Enum;

namespace TaskManager.Models
{
    public enum UnitFilterType
    {
        Status = 0,
        Assignee = 1,
        MileStone = 2,
        Author = 3,
        Label = 4,
        Project = 5,
        UserAccess = 6
    }

    public enum SortingType
    {
        Asc = 0,
        Desc = 1
    }

    public class SortingOptions
    {
        public Dictionary<string, SortingType> Sortings { get; set; }
    }

    public class FilterOptions
    {
        public Dictionary<UnitFilterType, dynamic> Filters { get; set; }
        public FilterOptions()
        {
            Filters = new Dictionary<UnitFilterType, dynamic>();
        }
    }
    public class SelectionOptions
    {
        public PagingOptions PagingOptions{ get; set; }
        public FilterOptions FilterOptions { get; set; }
        public SortingOptions SortingOptions { get; set; }
        public UnitType ExtendedType { get; set; }
        
        public SelectionOptions()
        {
            FilterOptions = new FilterOptions();
        }
    }

    public class PagingOptions
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }
    }
}
