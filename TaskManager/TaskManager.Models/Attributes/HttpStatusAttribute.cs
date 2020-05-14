using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models.Attributes
{
    public class HttpStatusAttribute : Attribute
    {
        public int Value { get; private set; }
        public HttpStatusAttribute(int statusCode)
        {
            Value = statusCode;
        }
    }
}
