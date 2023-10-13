using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.HousekeeperHelper
{
    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }
}
