using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerLibrary.Data.Interfaces
{
    public interface IMainRepository<TEntity> : IRepository<TEntity>
    {
       (List<TEntity>, int) ReadPage(int offset, int limit);

        int Count();
    }
}
