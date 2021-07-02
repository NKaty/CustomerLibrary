using System.Collections.Generic;

namespace CustomerLibrary.Data.Interfaces
{
    public interface IDependentRepository<TEntity> : IRepository<TEntity>
    {
        List<TEntity> ReadByCustomerId(int customerId);
        int CountByCustomerId(int customerId);
    }
}
