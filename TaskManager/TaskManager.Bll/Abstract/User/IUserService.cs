using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models.Response;
using TaskManager.Models.User;

namespace TaskManager.Bll.Abstract.User
{
    public interface IUserService
    {
        Task<DataResult<UserModel>> GetUser(int userId, int projectId);
    }
}
