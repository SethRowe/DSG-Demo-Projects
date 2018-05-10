using System.Collections.Generic;
using System.Threading.Tasks;
using DSG.UnityDI.Common;
using DSG.UnityDI.Common.Services;

namespace DSG.UnityDI.Repositories
{
    public class PromoRepository : IPromoRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public PromoRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Promo> GetItem(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task OutputUnitOfWorkIds(List<string> outputList)
        {
            await Task.Run(() => outputList.Add($"{nameof(PromoRepository)} UOW ID: {_unitOfWork.RandomId}"));
        }

    }
}