namespace Core.Utilities.Result
{
    public interface IResult
    {
        string Message { get; set; }
        bool Success { get; set; }
    }
}
