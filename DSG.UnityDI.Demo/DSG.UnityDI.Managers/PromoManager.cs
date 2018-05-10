using System.Collections.Generic;
using System.Threading.Tasks;
using DSG.UnityDI.Common;
using DSG.UnityDI.Common.Managers;
using DSG.UnityDI.Common.Services;

namespace DSG.UnityDI.Managers
{
    public class PromoManager : IPromoManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPromoRepository _promoRepository;
        private readonly ICouponManager _couponManager;

        public PromoManager(IUnitOfWork unitOfWork, IPromoRepository promoRepository, ICouponManager couponManager)
        {
            _unitOfWork = unitOfWork;
            _promoRepository = promoRepository;
            _couponManager = couponManager;
        }

        public Task<Promo> GetItem(int id, IUnitOfWork unitOfWork)
        {
            throw new System.NotImplementedException();
        }

        public async Task OutputUnitOfWorkIds(List<string> outputList)
        {
            outputList.Add($"{nameof(PromoManager)} UOW ID: {_unitOfWork.RandomId}");

            await _promoRepository.OutputUnitOfWorkIds(outputList);
            await _couponManager.OutputUnitOfWorkIds(outputList);
        }
    }
}