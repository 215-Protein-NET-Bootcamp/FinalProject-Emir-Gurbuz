namespace Core.Utilities.Result
{
    /// <summary>
    /// Paginated Response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingResult<T> : Result, IPagingResult<T>
    {
        public PagingResult(List<T> data, int totalItemCount, bool success, string message) : base(success, message)
        {
            Data = data;
            TotalItemCount = totalItemCount;
        }

        public List<T> Data { get; set; }
        public int TotalItemCount { get; set; }
    }
}
