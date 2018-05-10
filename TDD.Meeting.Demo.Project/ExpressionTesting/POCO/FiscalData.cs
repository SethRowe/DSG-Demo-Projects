using System;

namespace ExpressionTesting
{
    public class FiscalData : IFiscalData
    {
        public int FiscalWeek { get; set; }
        public int FiscalMonth { get; set; }
        public DateTime Date { get; set; }
    }
}