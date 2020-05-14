using AutoMapper;
using TaskManager.Dal.Abstract;

namespace TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy.Base
{
    public class BaseStrategy
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BaseStrategy(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
