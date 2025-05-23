using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHeap
{
    public static class SortingAlgorithms
    {
        public static void QuickSort(int[] array) => QuickSort(array, 0, array.Length - 1);

        private static void QuickSort(int[] array, int low, int high)
        {
            if (low >= high) return;
            int pivot = Partition(array, low, high);
            QuickSort(array, low, pivot - 1);
            QuickSort(array, pivot + 1, high);
        }

        private static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            int i = low;

            for (int j = low; j < high; j++)
            {
                if (array[j] < pivot)
                {
                    (array[i], array[j]) = (array[j], array[i]);
                    i++;
                }
            }

            (array[i], array[high]) = (array[high], array[i]);
            return i;
        }

        public static void HeapSort(int[] array)
        {
            var heap = new BinaryHeap<int>();
            foreach (var item in array)
                heap.Insert(item);

            for (int i = 0; i < array.Length; i++)
                array[i] = heap.Extract();
        }
    }

}
