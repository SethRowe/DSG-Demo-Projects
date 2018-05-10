using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSG.UnityDI.Common;
using DSG.UnityDI.Common.Managers;
using DSG.UnityDI.Common.Services;

namespace DSG.UnityDI.Managers
{
    public class CouponManager : ICouponManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICouponRepository _couponRepository;

        public CouponManager(IUnitOfWork unitOfWork, ICouponRepository couponRepository)
        {
            _unitOfWork = unitOfWork;
            _couponRepository = couponRepository;
        }

        public Task<Coupon> GetItem(int id, IUnitOfWork unitOfWork)
        {
            throw new NotImplementedException();
        }

        public async Task OutputUnitOfWorkIds(List<string> outputList)
        {
            outputList.Add($"{nameof(CouponManager)} UOW ID: {_unitOfWork.RandomId}");

            await _couponRepository.OutputUnitOfWorkIds(outputList);
        }
    }
}