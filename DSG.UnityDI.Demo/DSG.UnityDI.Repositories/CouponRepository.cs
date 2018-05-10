using System.Collections.Generic;
using System.Threading.Tasks;
using DSG.UnityDI.Common;
using DSG.UnityDI.Common.Services;

namespace DSG.UnityDI.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CouponRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Coupon> GetItem(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task OutputUnitOfWorkIds(List<string> outputList)
        {
            await Task.Run(() => outputList.Add($"{nameof(CouponRepository)} UOW ID: {_unitOfWork.RandomId}"));
        }

    }
}