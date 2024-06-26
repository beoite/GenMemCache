﻿namespace GenMemCacheApp
{
    public class TestThreading
    {
        private GenMemCache.Cache<System.Guid, string> cache = GenMemCache.Cache<System.Guid, string>.Instance;

        public TestThreading()
        {
            cache.Clear();

            cache.Capacity = 10;

            System.Threading.Tasks.Parallel.For(0, cache.Capacity * 4, i =>
            {
                Add(i);
            });

            cache.Log();
        }

        private void Add(int index)
        {
            string threadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();

            string data = nameof(index) + ": " + index.ToString() + " " + nameof(threadId) + ": " + threadId.ToString();

            System.Console.WriteLine(data);

            cache.Add(System.Guid.NewGuid(), data);
        }
    }
}