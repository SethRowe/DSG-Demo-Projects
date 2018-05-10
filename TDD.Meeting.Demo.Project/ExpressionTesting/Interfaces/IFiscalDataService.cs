using System;
using System.Threading.Tasks;

namespace ExpressionTesting
{
    public interface IFiscalDataService
    {
        Task<IFiscalData> GetFiscalData(DateTime dateTime);
        Task<int> CreateFiscalData(IFiscalData fiscalData);
    }
}