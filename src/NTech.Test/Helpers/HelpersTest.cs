using Core.Utilities.Security.Hashing;

namespace NTech.Test.Helpers
{
    public class HelpersTest
    {
        public HelpersTest()
        {

        }

        [Theory, InlineData("123456")]
        public void Password_hash_test_true(string password)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            bool result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);

            Assert.True(result);
        }
        [Theory, InlineData("123456", "654321")]
        public void Password_hash_test_false(string password1, string password2)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password2, out passwordHash, out passwordSalt);

            bool result = HashingHelper.VerifyPasswordHash(password1, passwordHash, passwordSalt);

            Assert.False(result);
        }
    }
}
