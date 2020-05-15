using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Enum;

namespace TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy
{
    public class UnitExtendedStrategyFactory
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOptions<MvcNewtonsoftJsonOptions> _jsonOptions;

        public UnitExtendedStrategyFactory(IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jsonOptions = jsonOptions;
        }

        public IUnitExtendedStrategy GetInstance(UnitType type)
        {
            IUnitExtendedStrategy current = null;
            switch (type)
            {
                case UnitType.Task:
                    current = new TaskExtendedStrategy(_unitOfWork.Tasks, _mapper, _jsonOptions);
                    break;

                case UnitType.Project:
                    current = new ProjectExtendedStrategy(_unitOfWork.Projects, _mapper, _jsonOptions);
                    break;

                default:
                    current = new RelationShipExtendedStrategy(_unitOfWork.RelationShips, _mapper, _jsonOptions);
                    break;
            }
            return current;
        }
    }
}
