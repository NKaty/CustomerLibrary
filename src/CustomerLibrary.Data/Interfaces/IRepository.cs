namespace CustomerLibrary.Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        int Create(TEntity entity);

        TEntity Read(int entityId);

        void Update(TEntity entity);

        void Delete(int entityId);
    }
}
