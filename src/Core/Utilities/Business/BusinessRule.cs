using Core.Utilities.Result;

namespace Core.Utilities.Business
{
    public class BusinessRule
    {
        public static IResult Run(params IResult[] results)
        {
            foreach (IResult result in results)
                if (result.Success == false)
                    return result;
            return null;
        }
    }
}
