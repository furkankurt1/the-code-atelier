using Core.Utilities.ResultWrapper;

namespace Core.Utilities
{
    public static class BusinessRules
    {
        public static async Task<IResult?> RunAsync(params Task<IResult>[] logics)
        {
            foreach (var logic in logics)
            {
                var result = await logic;
                if (!result.Success)
                {
                    return result;
                }
            }

            return null;
        }
    }
}