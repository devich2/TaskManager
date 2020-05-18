using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models.Response;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Abstract.Unit
{
    public interface IUnitEditService
    {
        Task<DataResult<UnitAddResponse>> ProcessUnitCreate(UnitCreateOrUpdateModel model);
        Task<DataResult<UnitUpdateResponse>> ProcessUnitUpdate(UnitCreateOrUpdateModel model);
        Task<DataResult<UnitUpdateResponse>> ProcessContentChangeStatus(UnitStatusPatchModel model);
        Task<Result> ProcessUnitDelete(int unitId);
    }
}
