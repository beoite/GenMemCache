namespace GenMemCacheApp
{
    public class TestCache
    {
        public static void Run()
        {
            // add some keys and data, then print the contents

            StringString();

            IntString();

            GuidString();
        }

        // Cache<string, string>
        private static void StringString()
        {
            GenMemCache.Cache<string, string> cache = GenMemCache.Cache<string, string>.Instance;

            cache.Clear();

            // won't work
            cache.Add(null, "");

            // won't work
            cache.Add("", null);

            cache.Add("1", "h");

            cache.Add("2", "e");

            cache.Add("3", "l");

            cache.Add("4", "l");

            cache.Add("5", "o");

            cache.Add("duplicate", "1");

            cache.Add("duplicate", "2");

            // this is the duplicate saved
            cache.Add("duplicate", "2");

            cache.Log();
        }

        // Cache<int, int>
        private static void IntString()
        {
            GenMemCache.Cache<int, string> cache = GenMemCache.Cache<int, string>.Instance;

            cache.Clear();

            for (int i = 0; i < 10; i++)
            {
                string data = Utility.RandomString(6);

                cache.Add(i, data);
            }

            cache.Log();
        }

        // Cache<Guid, string>
        private static void GuidString()
        {
            GenMemCache.Cache<System.Guid, string> cache = GenMemCache.Cache<System.Guid, string>.Instance;

            cache.Clear();

            for (ulong i = 0; i < 10; i++)
            {
                System.Guid key = System.Guid.NewGuid();

                string data = Utility.RandomString(6);

                cache.Add(key, data);
            }

            cache.Log();
        }
    }
}
