using System;
using System.Threading;
using System.Threading.Tasks;

namespace SortYourselfOut
{
    public class Sorting
    {
        public static void QuicksortSequential<T>(T[] arr, int left, int right)
            where T : IComparable<T>
        {
            if (right > left)
            {
                int pivot = Partition(arr, left, right);
                QuicksortSequential(arr, left, pivot - 1);
                QuicksortSequential(arr, pivot + 1, right);
            }
        }

        public static void QuicksortParallelOptimised<T>(T[] arr, int left, int right)
            where T : IComparable<T>
        {
            const int SEQUENTIAL_THRESHOLD = 2048;
            if (right > left)
            {
                if (right - left < SEQUENTIAL_THRESHOLD)
                {
                    QuicksortSequential(arr, left, right);
                }
                else
                {
                    int pivot = Partition(arr, left, right);
                    Parallel.Invoke(
                        () => QuicksortParallelOptimised(arr, left, pivot - 1),
                        () => QuicksortParallelOptimised(arr, pivot + 1, right));
                }
            }
        }

        static int Partition<T>(T[] arr, int low, int high) where T : IComparable<T>
        {
            int pivotPos = (high + low) / 2;
            T pivot = arr[pivotPos];
            Swap(arr, low, pivotPos);

            int left = low;
            for (int i = low + 1; i <= high; i++)
            {
                if (arr[i].CompareTo(pivot) < 0)
                {
                    left++;
                    Swap(arr, i, left);
                }
            }

            Swap(arr, low, left);
            return left;
        }

        static void Swap<T>(T[] arr, int i, int j)
        {
            T tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;
        }
    }
}