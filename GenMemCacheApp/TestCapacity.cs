namespace GenMemCacheApp
{
    public class TestCapacity
    {
        public TestCapacity()
        {
            GenMemCache.Cache<int, string> cache = GenMemCache.Cache<int, string>.Instance;

            cache.Clear();

            cache.Capacity = 42;

            cache.CapacityReached += CapacityReachedHandler;

            for (int i = 0; i < cache.Capacity * 2; i++)
            {
                string data = Utility.RandomString(6);

                cache.Add(i, data);
            }

            cache.Log();
        }

        private void CapacityReachedHandler(object? sender, GenMemCache.CapacityReachedEventArgs<int> e)
        {
            System.Console.WriteLine(nameof(CapacityReachedHandler) + " " + sender?.GetType().ToString() + " " + e.Message);
        }
    }
}
