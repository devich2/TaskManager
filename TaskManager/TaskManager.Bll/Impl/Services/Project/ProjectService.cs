using AutoMapper;
using TaskManager.Bll.Abstract.Unit;
using TaskManager.Dal.Abstract;

namespace TaskManager.Bll.Impl.Services.Project
{
    public class ProjectService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitSelectionService _unitSelectionService;

        public ProjectService(IMapper mapper,
            IUnitOfWork unitOfWork,
            IUnitSelectionService unitSelectionService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _unitSelectionService = unitSelectionService;
        }
        
    }
}
