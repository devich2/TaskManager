using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models.Response
{
    public class ExceptionResult : Result
    {
        public string StackTrace { get; set; }
    }
}
