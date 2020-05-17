using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Dal.Abstract.IRepository.Base;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Abstract.IRepository
{
    public interface ITermInfoRepository: IUnitFkRepository<TermInfo>
    {
    }
}
