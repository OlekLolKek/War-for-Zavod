namespace Utils
{
    public interface IAwaitable<TResult>
    {
        IAwaiter<TResult> GetAwaiter();
    }
}