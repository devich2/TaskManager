using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entities.Enum;
using TaskManager.Models;
using TaskManager.Models.Result;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Abstract.Unit
{
    public interface IUnitSelectionService
    {
        Task<DataResult<List<UnitSelectionModel>>> GetUnitPreview(SelectionOptions options); 
        Task<DataResult<UnitSelectionModel>> GetUnitById(int unitId);
        Task<List<Entities.Tables.Unit>> GetFilteredUnits(SelectionOptions options);
    }
}
