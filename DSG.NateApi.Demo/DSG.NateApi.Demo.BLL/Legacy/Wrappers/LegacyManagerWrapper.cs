using DSG.NateApi.Demo.BLL.Interfaces;

namespace DSG.NateApi.Demo.BLL.Legacy.Wrappers
{
    public class LegacyManagerWrapper : ILegacyManager
    {
        public void DoTheWork()
        {
            LegacyManager.DoTheWork();
        }

        public void DoSomeMoreWork()
        {
            // New code now
        }

        public void DoEvenMoreWork()
        {
            LegacyManager.DoEvenMoreWork();
        }

        public void DoSomeProperlyCodedWork()
        {
            throw new System.NotImplementedException();
        }
    }
}