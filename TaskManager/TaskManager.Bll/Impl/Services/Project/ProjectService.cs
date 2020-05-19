using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TaskManager.Bll.Abstract.Project;
using TaskManager.Dal.Abstract;

namespace TaskManager.Bll.Impl.Services.Project
{
    public class ProjectService: IProjectService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public 
    }
}
