using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DSG.TDD.StarterProject.Managers.Entities;

namespace DSG.TDD.StarterProject.Managers.Managers
{
    public class ProductManager : IProductManager
    {
        public Task<Product> GetItem(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetItems(Expression<Func<Product>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<long> SaveItem(Product item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(long id)
        {
            throw new NotImplementedException();
        }
    }
}