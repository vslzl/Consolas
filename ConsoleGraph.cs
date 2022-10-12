namespace Consolas;

public static class ConsoleGraph
{
    public static void Test(int arrayLength, int maxValue, int delayMs)
    {
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            Console.SetWindowPosition(0, 0);
            if (Console.WindowHeight < maxValue)
                Console.WindowHeight = maxValue + 10;
            if (Console.WindowWidth < (arrayLength))
                Console.WindowWidth = (arrayLength) + 10;
        }

        var originalData = GetRandomData(arrayLength, maxValue);
        var sortedData = originalData.Select(p => p).ToArray();
        var iterations = 0;
        Console.WriteLine($"SelectionSort");
        (sortedData, iterations) = SelectionSort(originalData.Select(p => p).ToArray(), delayMs);
        Visualize(sortedData, false);
        Console.WriteLine();
        Console.WriteLine($"SelectionSort iteration count:{iterations}");

        Console.WriteLine($"InsertionSort");
        (sortedData, iterations) = InsertionSort(originalData.Select(p => p).ToArray(), delayMs);
        Visualize(sortedData, false);
        Console.WriteLine();
        Console.WriteLine($"InsertionSort iteration count:{iterations}");


        Console.WriteLine($"HeapSort");
        (sortedData, iterations) = HeapSort(originalData.Select(p => p).ToArray(), delayMs);
        Visualize(sortedData, false);
        Console.WriteLine();
        Console.WriteLine($"Heap iteration count:{iterations}");


        Console.WriteLine($"ShellSort");
        (sortedData, iterations) = ShellSort(originalData.Select(p => p).ToArray(), delayMs);
        Visualize(sortedData, false);
        Console.WriteLine();
        Console.WriteLine($"Shell iteration count:{iterations}");


        Console.WriteLine($"CountingSort");
        (sortedData, iterations) = CountingSort(originalData.Select(p => p).ToArray(), delayMs);
        Visualize(sortedData, false);
        Console.WriteLine();
        Console.WriteLine($"CountingSort iteration count:{iterations}");

        Console.WriteLine($"RadixSort");
        (sortedData, iterations) = RadixSort(originalData.Select(p => p).ToArray(), delayMs);
        Visualize(sortedData, false);
        Console.WriteLine();
        Console.WriteLine($"RadixSort iteration count:{iterations}");


        Console.WriteLine($"BubbleSort");
        (sortedData, iterations) = BubbleSort(originalData.Select(p => p).ToArray(), delayMs / 10);
        Visualize(sortedData, false);
        Console.WriteLine();
        Console.WriteLine($"Bubble iteration count:{iterations}");
    }

    private static int[] GetRandomData(int count, int maxValue)
    {
        var random = new Random();
        return Enumerable.Repeat(0, count).Select(p => random.Next(0, maxValue)).ToArray();
    }

    static void Visualize(int[] arr, bool resetCursor = true)
    {
        var max = arr.Max();
        bool[,] pixelArray = new bool[max, arr.Length];

        for (int k = pixelArray.GetLength(0); k > 0; k--)
        {
            for (int l = 0; l < pixelArray.GetLength(1); l++)
            {
                pixelArray[(pixelArray.GetLength(0) - k), l] = arr[l] >= k ? true : false;
            }
        }
        for (int k = 0; k < pixelArray.GetLength(0); k++)
        {
            for (int l = 0; l < pixelArray.GetLength(1); l++)
            {
                Console.Write($"{(pixelArray[k, l] ? '*' : ' ')}");
            }
            Console.WriteLine();
        }
        if (resetCursor)
            Console.SetCursorPosition(0, Console.CursorTop - max);
    }


    static (int[] array, int iterations) SelectionSort(int[] arr, int delayMs)
    {
        int n = arr.Length;
        int counter = 0;

        // One by one move boundary of 
        // unsorted subarray 
        for (int i = 0; i < n - 1; i++)
        {
            // Find the minimum element 
            // in unsorted array 
            int min_idx = i;
            for (int j = i + 1; j < n; j++)
                if (arr[j] < arr[min_idx])
                    min_idx = j;

            // Swap the found minimum 
            // element with the first 
            // element 
            int temp = arr[min_idx];
            arr[min_idx] = arr[i];
            arr[i] = temp;
            counter++;
            Visualize(arr);
            Thread.Sleep(delayMs);
        }

        return (arr, counter);
    }

    static (int[] array, int iterations) InsertionSort(int[] arr, int delayMs)
    {
        int n = arr.Length, counter = 0;
        for (int i = 1; i < n; ++i)
        {
            int key = arr[i];
            int j = i - 1;

            // Move elements of arr[0..i-1], 
            // that are greater than key, 
            // to one position ahead of 
            // their current position 
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j = j - 1;
            }
            arr[j + 1] = key;
            counter++;
            Visualize(arr);
            Thread.Sleep(delayMs);
        }

        return (arr, counter);
    }


    static (int[] array, int iterations) BubbleSort(int[] arr, int delayMs)
    {
        int n = arr.Length;
        int counter = 0;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
                if (arr[j] > arr[j + 1])
                {
                    // swap temp and arr[i] 
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                    counter++;
                    Visualize(arr);
                }
        }

        return (arr, counter);
    }


    static (int[] array, int iterations) HeapSort(int[] array, int delayMs)
    {
        int heapSize = array.Length, counter = 0;

        Visualize(array);
        Thread.Sleep(delayMs*5);
        buildMaxHeap(array);
        Visualize(array);

        for (int i = heapSize - 1; i >= 1; i--)
        {
            swap(array, i, 0);
            heapSize--;
            sink(array, heapSize, 0);
            counter++;
            Visualize(array);
            Thread.Sleep(delayMs);
        }

        return (array, counter);
    }

    private static void buildMaxHeap<T>(T[] array) where T : IComparable<T>
    {
        int heapSize = array.Length;

        for (int i = (heapSize / 2) - 1; i >= 0; i--)
        {
            sink(array, heapSize, i);
        }
    }

    private static void sink<T>(T[] array, int heapSize, int toSinkPos) where T : IComparable<T>
    {
        if (getLeftKidPos(toSinkPos) >= heapSize)
        {
            // No left kid => no kid at all
            return;
        }


        int largestKidPos;
        bool leftIsLargest;

        if (getRightKidPos(toSinkPos) >= heapSize || array[getRightKidPos(toSinkPos)].CompareTo(array[getLeftKidPos(toSinkPos)]) < 0)
        {
            largestKidPos = getLeftKidPos(toSinkPos);
            leftIsLargest = true;
        }
        else
        {
            largestKidPos = getRightKidPos(toSinkPos);
            leftIsLargest = false;
        }



        if (array[largestKidPos].CompareTo(array[toSinkPos]) > 0)
        {
            swap(array, toSinkPos, largestKidPos);

            if (leftIsLargest)
            {
                sink(array, heapSize, getLeftKidPos(toSinkPos));

            }
            else
            {
                sink(array, heapSize, getRightKidPos(toSinkPos));
            }
        }

    }

    private static void swap<T>(T[] array, int pos0, int pos1)
    {
        T tmpVal = array[pos0];
        array[pos0] = array[pos1];
        array[pos1] = tmpVal;
    }

    private static int getLeftKidPos(int parentPos)
    {
        return (2 * (parentPos + 1)) - 1;
    }

    private static int getRightKidPos(int parentPos)
    {
        return 2 * (parentPos + 1);
    }


    public static (int[] array, int iterations) ShellSort(int[] array, int delayMs)
    {
        var n = array.Length;
        var counter = 0;
        for (int interval = n / 2; interval > 0; interval /= 2)
        {
            for (int i = interval; i < n; i++)
            {
                var currentKey = array[i];
                var k = i;
                while (k >= interval && array[k - interval] > currentKey)
                {
                    array[k] = array[k - interval];
                    k -= interval;
                }
                array[k] = currentKey;
                counter++;
                Visualize(array);
                Thread.Sleep(delayMs);
            }
        }
        return (array, counter);
    }


    public static int GetMaxVal(int[] array, int size)
    {
        var maxVal = array[0];
        for (int i = 1; i < size; i++)
            if (array[i] > maxVal)
                maxVal = array[i];
        return maxVal;
    }
    public static (int[] array, int iterations) CountingSort(int[] array, int delayMs)
    {
        var size = array.Length;
        var counter = 0;
        var maxElement = GetMaxVal(array, size);
        var occurrences = new int[maxElement + 1];
        for (int i = 0; i < maxElement + 1; i++)
        {
            occurrences[i] = 0;
        }
        for (int i = 0; i < size; i++)
        {
            occurrences[array[i]]++;
        }
        for (int i = 0, j = 0; i <= maxElement; i++)
        {
            while (occurrences[i] > 0)
            {
                array[j] = i;
                j++;
                occurrences[i]--;
                counter++;
                Visualize(array);
                Thread.Sleep(delayMs);
            }
        }
        return (array, counter);
    }

    public static (int[] array, int iterations) RadixSort(int[] array, int delayMs)
    {
        var size = array.Length;
        var counter = 0;
        var maxVal = GetMaxVal(array, size);
        for (int exponent = 1; maxVal / exponent > 0; exponent *= 10)
        {
            var outputArr = new int[size];
            var occurences = new int[10];
            for (int i = 0; i < 10; i++)
                occurences[i] = 0;
            for (int i = 0; i < size; i++)
                occurences[(array[i] / exponent) % 10]++;
            for (int i = 1; i < 10; i++)
                occurences[i] += occurences[i - 1];
            for (int i = size - 1; i >= 0; i--)
            {
                outputArr[occurences[(array[i] / exponent) % 10] - 1] = array[i];
                occurences[(array[i] / exponent) % 10]--;
            }
            for (int i = 0; i < size; i++)
            {
                array[i] = outputArr[i];
                counter++;
                Visualize(array);
                Thread.Sleep(delayMs);
            }
        }
        return (array, counter);
    }



}