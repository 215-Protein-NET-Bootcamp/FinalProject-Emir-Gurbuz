using System.Reflection;

namespace Core.Extensions
{
    public static class BusinessExtensions
    {
        public static void SetUserId(this object obj, object value)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty("UserId");

            ArgumentNullException.ThrowIfNull(propertyInfo);

            propertyInfo.SetValue(obj, value);
        }
    }
}
