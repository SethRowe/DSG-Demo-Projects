using System;

namespace ExpressionTesting
{
    public interface IFiscalData
    {
        int FiscalWeek { get; set; }
        int FiscalMonth { get; set; }
        DateTime Date { get; set; }
    }
}