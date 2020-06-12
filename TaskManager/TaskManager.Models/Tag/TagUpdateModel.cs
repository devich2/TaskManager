using System;
using System.Collections.Generic;

namespace TaskManager.Models.Tag
{
    public class TagUpdateModel
    {
        public int TaskId {get;set;}
        public List<string> Tags {get;set;}
    }
}