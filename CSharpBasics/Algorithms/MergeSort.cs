using System;
namespace CSharpBasics.Algorithms
{
    public static class MergeSort
    {
        public static void IntSort(int[] array)
        {
            Sort(array, new int[array.Length], 0, array.Length - 1);
        }

        private static void Sort(int[] array, int[] temp, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int middle = (start + end) / 2;

            // Sort left.
            Sort(array, temp, start, middle);
            // Sort right.
            Sort(array, temp, middle + 1, end);
            // Merge.
            Merge(array, temp, start, end);
        }

        private static void Merge(int[] array, int[] temp, int start, int end)
        {
            int middle = (start + end) / 2;
            int size = end - start + 1;

            int left = start;
            int right = middle + 1;
            int index = start;

            while (left <= middle && right <= end)
            {
                if (array[left] <= array[right])
                {
                    temp[index++] = array[left++];
                }
                else
                {
                    temp[index++] = array[right++];
                }
            }

            // Copy over the remainder of the elements from left side (if any left).
            Array.Copy(array, left, temp, index, middle - left + 1);
            // Do the same for any remaining on the right side.
            Array.Copy(array, right, temp, index, end - right + 1);
            // Copy from temp back into the array.
            Array.Copy(temp, start, array, start, size);
        }
    }
}
