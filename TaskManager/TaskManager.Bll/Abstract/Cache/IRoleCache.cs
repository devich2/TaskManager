namespace TaskManager.Bll.Abstract.Cache
{
    public interface IRoleCache
    {
        public decimal GetRankByRoleName(string name);
    }
}