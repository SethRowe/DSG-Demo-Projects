using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpressionTesting
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetItems(Expression<Func<T, bool>> predicate);
        Task<int> CreateItem(T item);
    }
}