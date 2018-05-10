using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSG.UnityDI.Common.Managers
{
    public interface IManager<T>
    {
        Task<T> GetItem(int id, IUnitOfWork unitOfWork);
        Task OutputUnitOfWorkIds(List<string> outputList);
    }
}