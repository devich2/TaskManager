using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models.Response
{
    public class DataResult<T> : Result
    {
        public T Data { get; set; }
    }
}
