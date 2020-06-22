using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskManager.Bll.Abstract.Email;
using TaskManager.Bll.Abstract.NewsLetter;
using TaskManager.Dal.Abstract;
using TaskManager.Email.Template.Engine.Views.Email;
using TaskManager.Email.Template.Engine.Views.Email.TaskExpiration;
using TaskManager.Models.Task;

namespace TaskManager.Bll.Impl.Services.NewsLetter
{
    public class NewsLetterService : INewsLetterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IMapper _mapper;

        public NewsLetterService(IUnitOfWork unitOfWork,
            IEmailTemplateService emailTemplateService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailTemplateService = emailTemplateService;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task NotifyTaskExpiration()
        {
            DateTimeOffset now = DateTimeOffset.Now;
            List<TaskExpirationModel> expirationModels =
                await _unitOfWork.Tasks.GetActiveTasksInDuePeriod(now.AddDays(1));
            List<EmailTemplateModel> emailTemplateModels = expirationModels.Select(x => new EmailTemplateModel()
            {
                Email = x.Email,
                Model = _mapper.Map<TaskExpirationModel, TaskExpirationEmailModel>
                (x, opt =>
                    opt.AfterMap((src, dest) =>
                        dest.ProjectTasks.ForEach(p => p.Tasks.
                            ForEach(t => t.Expired = t.DueTs < now))))
            }).ToList();

            await _emailTemplateService.TemplateEmailMessage(emailTemplateModels);
        }
    }
}