using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models.Response
{
    public class Result
    {
        public ResponseMessageType Message { get; set; }
        public string MessageDetails { get; set; }
        public ResponseStatusType ResponseStatusType { get; set; }
    }
}
