namespace Core.Utilities.Result
{
    public interface IPagingResult<T> : IResult
    {
        List<T> Data { get; set; }
        int TotalItemCount { get; set; }
    }
}
