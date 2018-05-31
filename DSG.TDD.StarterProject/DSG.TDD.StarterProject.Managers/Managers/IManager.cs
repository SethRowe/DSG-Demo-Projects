using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DSG.TDD.StarterProject.Managers.Managers
{
    public interface IManager<T>
    {
        Task<T> GetItem(long id);
        Task<IEnumerable<T>> GetItems();
        Task<IEnumerable<T>> GetItems(Expression<Func<T>> predicate);
        Task<long> SaveItem(T item);
        Task DeleteItem(long id);
    }
}