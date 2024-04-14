namespace GenMemCacheApp
{
    public class TestCapacity
    {
        private GenMemCache.Cache<int, string> cache = GenMemCache.Cache<int, string>.Instance;

        public TestCapacity()
        {
            cache.Clear();

            cache.Capacity = 10;

            cache.CapacityReached += CapacityReachedHandler;

            for (int i = 0; i < cache.Capacity * 2; i++)
            {
                string data = Utility.RandomString(6);

                cache.Add(i, data);
            }

            cache.Log();

            cache.CapacityReached -= CapacityReachedHandler;
        }

        private void CapacityReachedHandler(object? sender, GenMemCache.CapacityReachedEventArgs<int> e)
        {
            System.Console.WriteLine(nameof(CapacityReachedHandler) + " " + sender?.GetType().ToString() + " " + e.Message);
        }
    }
}