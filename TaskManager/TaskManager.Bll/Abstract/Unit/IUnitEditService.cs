using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Abstract.Unit
{
    public interface IUnitEditService
    {
        Task<DataResult<UnitAddResponse>> ProcessUnitCreate(UnitBlModel model);
        Task<DataResult<UnitUpdateResponse>> ProcessUnitUpdate(UnitBlModel model);
        Task<DataResult<UnitUpdateResponse>> ProcessContentChangeStatus(UnitStatusPatchModel model);
        Task<Result> ProcessUnitDelete(int unitId);
    }
}
