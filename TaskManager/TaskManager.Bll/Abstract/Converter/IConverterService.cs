using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Bll.Abstract.Converter
{
    public interface IConverterService<out T, in TK>
    {
        T Convert(TK attr);
    }
}
