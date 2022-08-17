namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string MethodName { get; set; }
        public List<LogParameter> Parameters { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
