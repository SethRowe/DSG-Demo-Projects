namespace DSG.UnityDI.Common
{
    public interface IUnitOfWork
    {
        int RandomId { get; }
        bool Commit();
    }
}