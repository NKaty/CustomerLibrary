namespace CustomerLibrary.BusinessLogic
{
    public interface IDependentService<TEntity> : IService<TEntity>
    {
        void Delete(TEntity entity);
    }
}
