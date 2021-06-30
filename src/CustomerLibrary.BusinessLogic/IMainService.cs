using System.Collections.Generic;

namespace CustomerLibrary.BusinessLogic
{
    public interface IMainService<TEntity> : IService<TEntity>
    {
        (List<TEntity>, int) ReadPage(int offset, int limit);

        int Count();

        void Delete(int entityId);
    }
}
