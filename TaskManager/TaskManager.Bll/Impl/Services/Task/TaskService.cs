using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TaskManager.Bll.Abstract.Task;
using TaskManager.Dal.Abstract;

namespace TaskManager.Bll.Impl.Services.Task
{
    public class TaskService: ITaskService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IMapper mapper, 
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public GetProjectById
    }
}
