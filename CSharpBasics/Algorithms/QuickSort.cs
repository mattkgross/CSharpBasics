using System;
namespace CSharpBasics.Algorithms
{
    public static class QuickSort
    {
        public static void IntSort(int[] array)
        {
            quicksort(array, 0, array.Length - 1);
        }

        private static void quicksort(int[] array, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            // For best results, we'll use a random pivot point in the bounds.
            // Ideally, it will be close to the median value. This should
            // average better than always just choosing the middle.
            int pivot = array[new Random().Next(left, right)];
            int index = partition(array, left, right, pivot);

            quicksort(array, left, index - 1);
            quicksort(array, index, right);
        }

        private static int partition(int[] array, int left, int right, int pivot)
        {
            while (left <= right)
            {
                while (array[left] < pivot)
                {
                    left++;
                }

                while (array[right] > pivot)
                {
                    right--;
                }

                if (left <= right)
                {
                    swap(array, left, right);
                    left++;
                    right--;
                }
            }

            return left;
        }

        private static void swap(int[] array, int left, int right)
        {
            int temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }
    }
}
