using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models.Response;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Abstract.Unit
{
    public interface IUnitSelectionService
    {
        public Task<DataResult<UnitSelectionModel>> GetUnitById(int unitId);
    }
}
