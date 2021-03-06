namespace CustomerLibrary.BusinessLogic
{
    public interface IService<TEntity>
    {
        int Create(TEntity entity);

        TEntity Read(int entityId);

        void Update(TEntity entity);
    }
}
