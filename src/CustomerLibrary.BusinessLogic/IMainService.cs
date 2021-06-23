﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerLibrary.BusinessLogic
{
    public interface IMainService<TEntity> : IService<TEntity>
    {
        List<TEntity> ReadAll();
    }
}