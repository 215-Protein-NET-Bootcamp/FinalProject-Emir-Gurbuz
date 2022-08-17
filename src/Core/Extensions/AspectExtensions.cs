using Castle.DynamicProxy;
using System.Reflection;

namespace Core.Extensions
{
    public static class AspectExtensions
    {
        public static bool CheckPasswordProperty(this object obja)
        {
            if (obja == null) return false;
            PropertyInfo[] properties = obja.GetType().GetProperties();
            if (properties.FirstOrDefault(p => p.Name.ToLower().Contains("password")) != null)
            {
                return true;
            }
            return false;
        }
        public static string GenerateMethodKey(this IInvocation invocation)
        {
            string methodName = string.Format("{0}.{1}", invocation.Method.ReflectedType.FullName, invocation.Method.Name);
            var parameters = invocation.Arguments.ToList();
            string key = $"{methodName}({string.Join(",", parameters.Select(x => x?.ToString() ?? "<Null>"))})";
            return key;
        }
    }
}
