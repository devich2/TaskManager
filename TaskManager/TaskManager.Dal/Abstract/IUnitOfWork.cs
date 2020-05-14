using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
        public IPermissionRepository Permissions { get; }
        public IProjectMemberRepository ProjectMembers { get; }
        public IProjectRepository Projects { get; }
        public IRelationShipRepository RelationShips { get; }
        public ITagOnTaskRepository TagOnTasks { get; }
        public ITagRepository Tags { get; }
        public ITaskRepository Tasks { get; }
        public ITermInfoRepository TermInfos { get; }
        public IUnitRepository Units { get; }
        public Task<int> SaveAsync();

    }
}
