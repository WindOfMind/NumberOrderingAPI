using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NumberOrdering.Domain;

namespace NumberOrdering.Infrastructure
{
    public class NumbersFileRepository : INumbersFileRepository
    {
        private readonly string _rootPath;

        public NumbersFileRepository(string rootPath = null)
        {
            _rootPath = rootPath ?? string.Empty;
        }

        public async Task<string> SaveAsync(int[] numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers));

            if (numbers.Length == 0)
                throw new ArgumentException("We cannot write empty array.", nameof(numbers));

            string path = Path.Combine(_rootPath, $"numbers_{ Guid.NewGuid()}.txt");
            await File.WriteAllTextAsync(path, string.Join(" ", numbers));

            return path;
        }

        public async Task<int[]> LoadAsync(string filePath)
        {
            if (filePath == null || !File.Exists(filePath))
            {
                return Enumerable.Empty<int>().ToArray();
            }

            string text = await File.ReadAllTextAsync(filePath);

            return text.Split(" ")
                .Select(int.Parse)
                .ToArray();
        }
    }
}
