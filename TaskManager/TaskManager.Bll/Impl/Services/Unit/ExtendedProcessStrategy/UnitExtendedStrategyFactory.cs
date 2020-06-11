﻿using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy
{
    public class UnitExtendedStrategyFactory: IUnitExtendedStrategyFactory
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Entities.Tables.Identity.User> _userManager;
        private readonly IMapper _mapper;
        private readonly IOptions<MvcNewtonsoftJsonOptions> _jsonOptions;

        public UnitExtendedStrategyFactory(IUnitOfWork unitOfWork,
            UserManager<Entities.Tables.Identity.User> userManager,
            IMapper mapper,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _jsonOptions = jsonOptions;
        }

        public IUnitExtendedStrategy GetInstance(UnitType type)
        {
            IUnitExtendedStrategy current;
            switch (type)
            {
                case UnitType.Task:
                    current = new TaskExtendedStrategy(_unitOfWork, _mapper, _jsonOptions);
                    break;

                case UnitType.Project:
                    current = new ProjectExtendedStrategy(_unitOfWork, _userManager, _mapper, _jsonOptions);
                    break;
                case UnitType.Milestone:
                    current = new MileStoneExtendedStrategy(_unitOfWork.MileStones, _mapper, _jsonOptions);
                    break;
                default:
                    current = new NoUnitExtendedStrategy(_jsonOptions);
                    break;
            }
            return current;
        }
    }
}
