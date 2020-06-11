using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl.ImplRepository.Base;
using TaskManager.Entities.Tables;

namespace TaskManager.Dal.Impl.ImplRepository
{
    public class TagRepository: GenericKeyRepository<int, Tag>, ITagRepository
    {
        public TagRepository(TaskManagerDbContext context) : base(context)
        {
        }
    }
}