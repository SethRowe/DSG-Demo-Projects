using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSG.UnityDI.Common.Services
{
    public interface IRepository<T>
    {
        Task<T> GetItem(int id);
        Task OutputUnitOfWorkIds(List<string> outputList);
    }
}