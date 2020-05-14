using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using TaskManager.Entities.Enum;

namespace TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base
{
    public interface IUnitExtendedStrategyFactory
    {
        IUnitExtendedStrategy GetInstance(UnitType type);
    }
}
