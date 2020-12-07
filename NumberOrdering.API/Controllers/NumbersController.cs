using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NumberOrdering.API.Models;
using NumberOrdering.Domain;
using NumberOrdering.Domain.Extensions;

namespace NumberOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumbersController : ControllerBase
    {
        private readonly ILogger<NumbersController> _logger;
        private readonly INumbersService _numbersService;

        public NumbersController(ILogger<NumbersController> logger, INumbersService numbersService)
        {
            _numbersService = numbersService ?? throw new ArgumentNullException(nameof(numbersService));
            _logger = logger;
        }

        /// <summary>
        /// Post numbers for ordering and saving.
        /// </summary>
        /// <param name="request"><see cref="PostNumbersRequest"/></param>
        /// <response code="200">Numbers successfully posted.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        public Task PostNumbersAsync([FromBody] PostNumbersRequest request)
        {
            int[] numbers = request.Numbers;

            _logger.Log(LogLevel.Information, $"Received {numbers.Length} numbers: {string.Join(" ", numbers)}.");

            numbers.Sort();

            return _numbersService.SaveAsync(numbers);
        }

        /// <summary>
        /// Get latest saved and ordered numbers.
        /// If previously no numbers are saved, a response with empty array will be returned.
        /// </summary>
        /// <returns>Response with ordered and saved numbers.</returns>
        [HttpGet]
        public async Task<GetNumbersResponse> GetLatestNumbersAsync()
        {
            var numbers = await _numbersService.GetLatestNumbersAsync();

            return new GetNumbersResponse
            {
                Numbers = numbers
            };
        }
    }
}
