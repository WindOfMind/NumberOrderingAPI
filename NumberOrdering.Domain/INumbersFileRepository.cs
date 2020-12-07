using System;
using System.Threading.Tasks;

namespace NumberOrdering.Domain
{
    public interface INumbersFileRepository
    {
        /// <summary>
        /// Save array into a file.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if passed argument is null.</exception>
        /// <exception cref="ArgumentException">Thrown if passed argument is empty.</exception>
        /// <param name="numbers">Array to be saved in a file.</param>
        /// <returns>Path of created file.</returns>
        Task<string> SaveAsync(int[] numbers);

        /// <summary>
        /// Load a content of a file with numbers.
        /// If file does not exists, the empty array will be return.
        /// </summary>
        /// <param name="filePath">A path to a file.</param>
        /// <returns>Array with numbers.</returns>
        Task<int[]> LoadAsync(string filePath);
    }
}
