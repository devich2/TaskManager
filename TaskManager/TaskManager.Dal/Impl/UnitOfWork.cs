using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Impl
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly TaskManagerDbContext _taskManagerDbContext;
        private bool _disposed;
        public UnitOfWork(TaskManagerDbContext taskManagerDbContext)
        {
            _taskManagerDbContext = taskManagerDbContext;
        }


        #region Fields

        private IPermissionRepository _permissionRepository;
        private IProjectRepository _projectRepository;
        private ITagOnTaskRepository _tagOnTaskRepository;
        private ITagRepository _tagRepository;
        private ITaskRepository _taskRepository;
        private ITermInfoRepository _termInfoRepository;
        private IUnitRepository _unitRepository;
        private IMileStoneRepository _mileStoneRepository;
        private IProjectMemberRepository _projectMemberRepository;
        #endregion

        #region Properties
        
        public IProjectMemberRepository ProjectMembers
        {
            get
            {
                return _projectMemberRepository ??= new ProjectMemberRepository(_taskManagerDbContext);
            }
        }
        
        public IPermissionRepository Permissions
        {
            get
            {
                return _permissionRepository ??= new PermissionRepository(_taskManagerDbContext);
            }
        }
        public IMileStoneRepository MileStones
        {
            get
            {
                return _mileStoneRepository ??= new MileStoneRepository(_taskManagerDbContext);
            }
        }
        public IProjectRepository Projects
        {
            get
            {
                return _projectRepository ??= new ProjectRepository(_taskManagerDbContext);
            }
        }

        public ITagOnTaskRepository TagOnTasks
        {
            get
            {
                return _tagOnTaskRepository ??= new TagOnTaskRepository(_taskManagerDbContext);
            }
        }

        public ITagRepository Tags
        {
            get
            {
                return _tagRepository ??= new TagRepository(_taskManagerDbContext);
            }
        }

        public ITaskRepository Tasks
        {
            get
            {
                return _taskRepository ??= new TaskRepository(_taskManagerDbContext);
                ;
            }
        }

        public ITermInfoRepository TermInfos
        {
            get
            {
                return _termInfoRepository ??= new TermInfoRepository(_taskManagerDbContext);
            }
        }

        public IUnitRepository Units
        {
            get
            {
                return _unitRepository ??= new UnitRepository(_taskManagerDbContext);
            }
        }

        #endregion

        #region Methods
        public Task<int> SaveAsync()
        {
            return _taskManagerDbContext.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _taskManagerDbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
