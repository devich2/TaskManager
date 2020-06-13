using System.Collections.Generic;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Seed
{
    public class PermissionEntitiesHolder
    {
        private readonly List<Permission> _permissions = new List<Permission>()
        {
            #region Guest

            new Permission()
            {
                RoleId = 1,
                PermissionType = PermissionType.Read
            },

            #endregion

            #region Developer

            new Permission()
            {
                RoleId = 2,
                PermissionType = PermissionType.Read
            },
            new Permission()
            {
                RoleId = 2,
                PermissionType = PermissionType.CommentAdd
            },
            new Permission()
            {
                RoleId = 2,
                PermissionType = PermissionType.CommentDelete
            },
            new Permission()
            {
                RoleId = 2,
                PermissionType = PermissionType.CommentModify
            },
            new Permission()
            {
                RoleId = 2,
                PermissionType = PermissionType.TagAdd
            },
            new Permission()
            {
                RoleId = 2,
                PermissionType = PermissionType.TagDelete
            },
            new Permission()
            {
                RoleId = 2,
                PermissionType = PermissionType.TagUpdate
            },

            #endregion

            #region Maintainer

            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.Read
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.CommentAdd
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.CommentDelete
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.CommentModify
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.MilestoneAdd
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.MilestoneDelete
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.MilestoneModify
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.TaskAdd
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.TaskDelete
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.TaskModify
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.SubTaskAdd
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.SubTaskDelete
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.SubTaskModify
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.TagAdd
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.TagDelete
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.TagUpdate
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.StatusChange
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.TaskMileStoneChange
            },
            new Permission()
            {
                RoleId = 3,
                PermissionType = PermissionType.TaskAssigneeChange
            },
            #endregion

            #region Owner

            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.Read
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.CommentAdd
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.CommentDelete
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.CommentModify
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.MilestoneAdd
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.MilestoneDelete
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.MilestoneModify
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.TaskAdd
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.TaskDelete
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.TaskModify
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.SubTaskAdd
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.SubTaskDelete
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.SubTaskModify
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.ProjectDelete
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.ProjectModify
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.RoleChange
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.UserInvite
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.UserKick
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.TagAdd
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.TagDelete
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.TagUpdate
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.StatusChange
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.TaskMileStoneChange
            },
            new Permission()
            {
                RoleId = 4,
                PermissionType = PermissionType.TaskAssigneeChange
            },
            
            #endregion
        };
        
        public List<Permission> GetPermissions()
        {   
            int id = 0;
            foreach (var pm in _permissions)
            {
                pm.Id = ++id;
            }
            return _permissions;
        }
    }
}