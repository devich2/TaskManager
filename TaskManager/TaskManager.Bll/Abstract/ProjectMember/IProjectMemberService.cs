﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using TaskManager.Entities.Enum;
using TaskManager.Models;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.User;

namespace TaskManager.Bll.Abstract.ProjectMember
{
    public interface IProjectMemberService
    {
        Task<DataResult<List<PermissionType>>> GetUserPermissions(int userId, int projectId);
        Task<string> GetUserProjectRole(int userId, int projectId);
        Task<bool> IsProjectMember(int projectId, int userId);
        Task<DataResult<ProjectMemberResponse>> AddToProject(ProjectMemberRoleModel model);
        Task<Result> RemoveFromProject(int projectId, int userId);
        Task<DataResult<UserResponse>> AddUserRole(int projectId, int userId, string roleName);
        Task<List<ProjectMemberDisplayModel>> GetProjectMembers(int projectId,
            SortingOptions sortingOptions, string searchString);
        Task<DataResult<ProjectMemberSelectionModel>> GetMembersList(int projectId, string searchString);
        Task<DataResult<RoleChangeResponse>> ChangeRole(int currentUserId, ProjectMemberRoleModel model);
    }
}