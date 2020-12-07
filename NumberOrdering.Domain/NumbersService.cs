using System;
using System.Linq;
using System.Threading.Tasks;

namespace NumberOrdering.Domain
{
    public class NumbersService : INumbersService
    {
        private readonly object _lock = new object();
        private readonly INumbersFileRepository _repository;

        private string _latestSavedFilePath;
        private DateTime _latestSavedOn;

        public NumbersService(INumbersFileRepository repository)
        {
            _repository = repository;
        }

        public async Task SaveAsync(int[] numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers));

            if (numbers.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(numbers));

            string path = await _repository.SaveAsync(numbers);
            DateTime saveOn = DateTime.Now;

            lock (_lock)
            {
                if (saveOn > _latestSavedOn)
                {
                    _latestSavedOn = saveOn;
                    _latestSavedFilePath = path;
                }
            }
        }

        public async Task<int[]> GetLatestNumbersAsync()
        {
            if (_latestSavedFilePath == null)
            {
                return Enumerable.Empty<int>().ToArray();
            }

            var path = new string(_latestSavedFilePath);

            return await _repository.LoadAsync(path);
        }
    }
}