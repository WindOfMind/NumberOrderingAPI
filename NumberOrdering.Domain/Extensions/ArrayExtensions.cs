using System;
using System.Linq;

namespace NumberOrdering.Domain.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Sort an input array using in-place QuickSort algorithm in ascending order.
        /// </summary>
        public static void Sort(this int[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            QuickSort(array, 0, array.Length - 1);
        }

        private static void QuickSort(int[] array, int start, int end)
        {
            if (array.Length == 1 || end - start + 1 <= 1)
                return;

            SetPivot(array, start, end);

            int pivotPos = PartitionAroundPivot(array, start, end);
            QuickSort(array, start, pivotPos - 1);
            QuickSort(array, pivotPos + 1, end);
        }

        private static void SetPivot(int[] array, int start, int end)
        {
            // Use median of three values strategy for selecting a pivot
            int length = end - start + 1;

            int midIdx = length % 2 == 0
                ? length / 2 - 1 + start
                : length / 2 + start;

            int pivotIndex = new[] { start, midIdx, end }
                .OrderBy(i => array[i])
                .First();

            Swap(array, start, pivotIndex);
        }

        private static int PartitionAroundPivot(int[] array, int start, int end)
        {
            int pivot = array[start];
            int left = start + 1;

            for (int right = start + 1; right <= end; right++)
            {
                if (array[right] < pivot)
                {
                    Swap(array, left, right);
                    left++;
                }
            }

            int pivotPos = left - 1;
            Swap(array, start, pivotPos);

            return pivotPos;
        }

        private static void Swap(int[] array, int idx1, int idx2)
        {
            int temp = array[idx2];
            array[idx2] = array[idx1];
            array[idx1] = temp;
        }
    }
}
