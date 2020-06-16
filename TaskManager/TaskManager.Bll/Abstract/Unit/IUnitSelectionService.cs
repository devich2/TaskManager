using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entities.Enum;
using TaskManager.Models;
using TaskManager.Models.Pagination;
using TaskManager.Models.Result;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Abstract.Unit
{
    public interface IUnitSelectionService
    {
        Task<SelectionModel<UnitSelectionModel>> GetUnitPreview(int userId, SelectionOptions options);
        Task<DataResult<UnitSelectionModel>> GetUnitById(int unitId);
    }
}
