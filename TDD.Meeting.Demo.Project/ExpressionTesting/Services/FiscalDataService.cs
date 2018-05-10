using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpressionTesting
{
    public class FiscalDataService : IFiscalDataService
    {
        private readonly IValidator<IFiscalData> _fiscalDataValidator;
        private readonly IFiscalDataRepository _fiscalDataRepository;

        public FiscalDataService(IFiscalDataRepository fiscalDataRepository, IValidator<IFiscalData> fiscalDataValidator)
        {
            _fiscalDataRepository = fiscalDataRepository;
            _fiscalDataValidator = fiscalDataValidator;
        }

        public async Task<IFiscalData> GetFiscalData(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                dateTime = DateTime.Today;
            
            var items = await _fiscalDataRepository.GetItems(fd => fd.Date == dateTime);

            if (items == null)
                throw new NoDataFoundException();

            return items.FirstOrDefault();
        }

        public async Task<int> CreateFiscalData(IFiscalData fiscalData)
        {
            if (fiscalData == null)
                throw new ArgumentNullException(nameof(fiscalData));

            if (!_fiscalDataValidator.IsValid(fiscalData))
                return -1;

            return await _fiscalDataRepository.CreateItem(fiscalData);
        }
    }
}