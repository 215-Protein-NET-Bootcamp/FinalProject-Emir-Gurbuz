using Core.Exceptions.Extension;
using System.Reflection;

namespace Core.Extensions
{
    public static class BusinessExtensions
    {
        public static void SetUserId(this object obj, object value, bool throwException = false)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty("UserId");

            if (throwException)
                NotFoundUserIdException.ThrowIfNull(propertyInfo);

            if (obj.GetType() != propertyInfo.PropertyType)
                throw new NotEqualPropertyTypeException();

            if (propertyInfo is not null)
                propertyInfo.SetValue(obj, value);
        }
    }
}
