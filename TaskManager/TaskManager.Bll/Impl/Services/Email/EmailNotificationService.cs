using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskManager.Bll.Abstract.Email;
using TaskManager.Dal.Abstract;
using TaskManager.Email.Template.Engine.Views.Email;
using TaskManager.Email.Template.Engine.Views.Email.ProjectInvitation;
using TaskManager.Email.Template.Engine.Views.Email.TaskExpiration;
using TaskManager.Models.Task;

namespace TaskManager.Bll.Impl.Services.Email
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly UserManager<Entities.Tables.Identity.User> _userManager;
        private readonly IMapper _mapper;

        public EmailNotificationService(IUnitOfWork unitOfWork,
            IEmailTemplateService emailTemplateService,
            UserManager<Entities.Tables.Identity.User> userManager,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailTemplateService = emailTemplateService;
            _userManager = userManager;
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
        
        public async System.Threading.Tasks.Task SendInvitation(int projectId, int userId)
        {
            Entities.Tables.Unit project = await _unitOfWork.Units.GetByIdAsync(projectId);
            Entities.Tables.Identity.User user = await _userManager.FindByIdAsync(userId.ToString());
            if(project != null && user != null)
            {
                List<EmailTemplateModel> emailTemplateModels = new List<EmailTemplateModel>()
                {
                    new EmailTemplateModel()
                    {
                        Email = user.Email,
                        Model = new ProjectInvitationModel()
                        {
                            ProjectId = projectId,
                            ProjectName = project.Name,
                            ProjectDesc = project.Description
                        }
                    }
                };
                await _emailTemplateService.TemplateEmailMessage(emailTemplateModels);
            }
        }
    }
}