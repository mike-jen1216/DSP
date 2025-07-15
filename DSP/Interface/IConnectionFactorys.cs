using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DSP.Interface
{
    public interface IConnectionFactorys
    {
        IDbConnection GetConnection();
    }
}