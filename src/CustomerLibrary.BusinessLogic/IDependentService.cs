using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerLibrary.BusinessLogic
{
    public interface IDependentService<TEntity> : IService<TEntity>
    {
        List<TEntity> ReadByCustomerId(int customerId);
    }
}
