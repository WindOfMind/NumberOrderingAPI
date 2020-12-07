using Swashbuckle.AspNetCore.Filters;

namespace NumberOrdering.API.Models
{
    public class GetNumbersResponseExample : IExamplesProvider<GetNumbersResponse>
    {
        public GetNumbersResponse GetExamples()
        {
            return new GetNumbersResponse
            {
                Numbers = new[] { 1, 2, 3, 4, 5, 7, 9 }
            };
        }
    }
}