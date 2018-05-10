using DSG.NateApi.Demo.DAL.Legacy.Interfaces;

namespace DSG.NateApi.Demo.DAL.Legacy.Wrappers
{
    public class LegacyRepositoryWrapper : ILegacyRepository
    {
        public void DoTheWork()
        {
            LegacyRepository.DoTheWork();
        }
    }
}