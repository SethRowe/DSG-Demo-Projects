namespace ExpressionTesting
{
    public interface IValidator<in T> where T : class
    {
        bool IsValid(T item);
    }
}