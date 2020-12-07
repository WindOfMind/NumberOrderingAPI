using System;
using System.Threading.Tasks;

namespace NumberOrdering.Domain
{
    public interface INumbersService
    {
        /// <summary>
        /// Save array into a file.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if passed argument is null.</exception>
        /// <exception cref="ArgumentException">Thrown if passed argument is empty.</exception>
        /// <param name="numbers">Array to be saved in a file.</param>
        Task SaveAsync(int[] numbers);

        /// <summary>
        /// Return latest saved numbers.
        /// If no numbers were saved, it will return empty array.
        /// </summary>
        /// <returns>Array with numbers.</returns>
        Task<int[]> GetLatestNumbersAsync();
    }
}