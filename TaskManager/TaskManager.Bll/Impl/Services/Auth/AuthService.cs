using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32.SafeHandles;
using TaskManager.Bll.Abstract.Auth;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.Transactions;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.Auth;
using TaskManager.Models.Result;
using TaskManager.Models.User;

namespace TaskManager.Bll.Impl.Services.Auth
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<Entities.Tables.Identity.User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITransactionManager _transactionManager;
        private readonly SignInManager<Entities.Tables.Identity.User> _signInManager;

        public AuthService(
            UserManager<Entities.Tables.Identity.User> userManager,
            SignInManager<Entities.Tables.Identity.User> signInManager,
            IMapper mapper,
            ITransactionManager transactionManager
        )
        {
            _userManager = userManager;
            _mapper = mapper;
            _transactionManager = transactionManager;
            _signInManager = signInManager;
        }

        public async System.Threading.Tasks.Task<DataResult<UserBaseModel>> Login(LoginModel model)
        {
            DataResult<UserBaseModel> result = new DataResult<UserBaseModel>();

            Entities.Tables.Identity.User user = await _userManager.FindByEmailAsync(model.Email.Trim());
            if (user == null || !(await _signInManager.CheckPasswordSignInAsync(user, model.Password, false)).Succeeded)
            {
                result.ResponseStatusType = ResponseStatusType.Error;
                result.Message = ResponseMessageType.UserNotAuthorized;
            }
            else
            {
                await _signInManager.RefreshSignInAsync(user);
                UserBaseModel userModel = _mapper.Map<UserBaseModel>(user);
                result.ResponseStatusType = ResponseStatusType.Succeed;
                result.Data = userModel;
            }
            return result;
        }
        
        public async System.Threading.Tasks.Task<Result> Register(RegisterModel model)
        {
            Entities.Tables.Identity.User user = _mapper.Map<Entities.Tables.Identity.User>(model);
            
            return await _transactionManager.ExecuteInImplicitTransactionAsync(async () => {
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if(!result.Succeeded)
                {
                    return new Result()
                    {
                        ResponseStatusType = ResponseStatusType.Error,
                        Message = ResponseMessageType.InternalError
                    };
                }
                /*
                var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var emailConfirmUrl = urlsOptions.CurrentValue.Api
                    .AppendPathSegments("Auth", "ConfirmRegister")
                    .SetQueryParams(new {uid = user.Id, token = confirmToken});

               
                    await emailSenderService.SendEmailByTemplateAsync(
                        model.Email,
                        "register.html",
                        new Dictionary<string, string> {{"[link]", emailConfirmUrl}}
                    );
            */
                return new Result()
                {
                    ResponseStatusType = ResponseStatusType.Succeed
                };
            });
        }
    }
}