﻿namespace GenMemCacheApp
{
    public class MainLoop : GenMemCache.Logger
    {
        private bool sentinel = false;

        private GenMemCache.Cache<int, string> cache = GenMemCache.Cache<int, string>.Instance;

        private int capacity = 10;

        // test performance / memory leaks, replace the entire cache every frame, print some metrics
        public MainLoop()
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            System.Console.Clear();

            cache.Clear();

            cache.Capacity = capacity;

            while (sentinel == false)
            {
                long startTicks = System.DateTime.Now.Ticks;

                System.Threading.Tasks.Parallel.For(0, capacity, i =>
                {
                    string data = Utility.RandomString(capacity);

                    cache.Add(i, data);
                });

                System.Console.SetCursorPosition(0, 0);

                long elapsed = System.DateTime.Now.Ticks - startTicks;

                double elapsedMilliseconds = System.TimeSpan.FromTicks(elapsed).Milliseconds;

                double msPerOperation = elapsedMilliseconds / capacity;

                string fmt = "0.00000000";
                string text = string.Empty;
                text += nameof(capacity) + " " + capacity.ToString() + " ";
                text += nameof(elapsedMilliseconds) + " " + elapsedMilliseconds.ToString(fmt) + " ";
                text += nameof(msPerOperation) + " " + msPerOperation.ToString(fmt) + " ms ";
                Log(text);

                LogMemory();

                ConsoleWriteLine();

                Clear();

                System.Threading.Thread.Sleep(100);
            }
        }

        private void LogMemory()
        {
            using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
            {
                int decimalPlaces = 8;

                // The amount of system memory, in bytes, allocated for the associated process that cannot be written to the virtual memory paging file.
                Log(nameof(process.NonpagedSystemMemorySize64) + "\t" + Utility.SizeSuffix(process.NonpagedSystemMemorySize64, decimalPlaces));

                // The amount of memory, in bytes, allocated in the virtual memory paging file for the associated process.
                Log(nameof(process.PagedMemorySize64) + "\t\t" + Utility.SizeSuffix(process.PagedMemorySize64, decimalPlaces));

                // The amount of system memory, in bytes, allocated for the associated process that can be written to the virtual memory paging file.
                Log(nameof(process.PagedSystemMemorySize64) + "\t\t" + Utility.SizeSuffix(process.PagedSystemMemorySize64, decimalPlaces));

                // The maximum amount of memory, in bytes, allocated in the virtual memory paging file for the associated process since it was started.
                Log(nameof(process.PeakPagedMemorySize64) + "\t\t" + Utility.SizeSuffix(process.PeakPagedMemorySize64, decimalPlaces));

                // The maximum amount of virtual memory, in bytes, allocated for the associated process since it was started.
                Log(nameof(process.PeakVirtualMemorySize64) + "\t\t" + Utility.SizeSuffix(process.PeakVirtualMemorySize64, decimalPlaces));

                // The maximum amount of physical memory, in bytes, allocated for the associated process since it was started.
                Log(nameof(process.PeakWorkingSet64) + "\t\t" + Utility.SizeSuffix(process.PeakWorkingSet64, decimalPlaces));

                // The amount of memory, in bytes, allocated for the associated process that cannot be shared with other processes.
                Log(nameof(process.PrivateMemorySize64) + "\t\t" + Utility.SizeSuffix(process.PrivateMemorySize64, decimalPlaces));

                // The amount of virtual memory, in bytes, allocated for the associated process.
                Log(nameof(process.VirtualMemorySize64) + "\t\t" + Utility.SizeSuffix(process.VirtualMemorySize64, decimalPlaces));

                // The amount of physical memory, in bytes, allocated for the associated process.
                Log(nameof(process.WorkingSet64) + "\t\t\t" + Utility.SizeSuffix(process.WorkingSet64, decimalPlaces));
            }
        }
    }
}
