using Core.Exceptions.Extension;
using Core.Extensions;
using NTech.Entity.Concrete;

namespace NTech.Test.Extension
{
    public class ExtensionsTest
    {
        public ExtensionsTest()
        {

        }
        [Theory]
        [InlineData(1)]
        public void Set_user_id_value(int userId)
        {
            Product product = new();

            product.SetUserId(userId);

            Assert.Equal(userId, product.UserId);
        }
        [Theory, InlineData(1)]
        public void Set_user_id_null_exception(int userId)
        {
            Assert.Throws(typeof(NotFoundUserIdException), () =>
            {
                Category category = new();

                category.SetUserId(userId);
            });
        }
    }
}
