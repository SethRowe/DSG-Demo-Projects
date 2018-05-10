using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using DSG.UnityDI.Common;
using DSG.UnityDI.Common.Managers;
using DSG.UnityDI.Managers;

namespace DSG.UnityDI.Demo.Controllers
{
    [RoutePrefix("api")]
    public class PromosController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPromoManager _promoManager;
        private readonly ICouponManager _couponManager;

        public PromosController(IUnitOfWork unitOfWork, IPromoManager promoManager, ICouponManager couponManager)
        {
            _unitOfWork = unitOfWork;
            _promoManager = promoManager;
            _couponManager = couponManager;
        }

        [Route("promos")]
        public async Task<IEnumerable<string>> Get()
        {
            return await Task.FromResult(new [] { "value1", "value2" });
        }

        [Route("promos/uow_ids")]
        public async Task<IEnumerable<string>> GetUnitOfWorkIds()
        {
            var outputList = new List<string>
            {
                $"{nameof(PromosController)} UOW ID: {_unitOfWork.RandomId}"
            };

            await _promoManager.OutputUnitOfWorkIds(outputList);
            await _couponManager.OutputUnitOfWorkIds(outputList);

            return outputList;
        }
    }
}
