using Swashbuckle.AspNetCore.Filters;

namespace NumberOrdering.API.Models
{
    public class PostNumbersRequestExample : IExamplesProvider<PostNumbersRequest>
    {
        public PostNumbersRequest GetExamples()
        {
            return new PostNumbersRequest
            {
                Numbers = new [] {1, 9, 2, 3, 7, 4, 5}
            };
        }
    }
}
