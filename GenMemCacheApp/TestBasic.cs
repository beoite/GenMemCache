namespace GenMemCacheApp
{
    public class TestBasic
    {
        public TestBasic()
        {
            // add some keys and data, then print the contents

            StringString();

            IntString();

            GuidString();

            // test the Get method

            TestGet();
        }

        private void StringString()
        {
            GenMemCache.Cache<string, string> cache = GenMemCache.Cache<string, string>.Instance;

            cache.Clear();

            cache.Capacity = 10;

            // won't work
            cache.Add(null, "");

            // won't work
            cache.Add("", null);

            cache.Add("1", "h");

            cache.Add("2", "e");

            cache.Add("3", "l");

            cache.Add("4", "l");

            cache.Add("5", "o");

            string duplicate = "duplicate";

            cache.Add(duplicate, "1");

            cache.Add(duplicate, "2");

            cache.Add(duplicate, "3");

            cache.Log();
        }

        private void IntString()
        {
            GenMemCache.Cache<int, string> cache = GenMemCache.Cache<int, string>.Instance;

            cache.Clear();

            cache.Capacity = 10;

            for (int i = 0; i < 10; i++)
            {
                string data = Utility.RandomString(6);

                cache.Add(i, data);
            }

            cache.Log();
        }

        private void GuidString()
        {
            GenMemCache.Cache<System.Guid, string> cache = GenMemCache.Cache<System.Guid, string>.Instance;

            cache.Clear();

            cache.Capacity = 10;

            for (ulong i = 0; i < 10; i++)
            {
                System.Guid key = System.Guid.NewGuid();

                string data = Utility.RandomString(6);

                cache.Add(key, data);
            }

            cache.Log();
        }

        private void TestGet()
        {
            GenMemCache.Cache<string, string> cache = GenMemCache.Cache<string, string>.Instance;

            cache.Clear();

            cache.Capacity = 5;

            for (int i = 0; i < Utility.Chars.Length; i++)
            {
                string key = Utility.RandomString(1);

                string? data = cache.Get(key);

                bool isCached = data is not null;

                if (isCached == true)
                {
                    System.Console.WriteLine(key + " " + nameof(isCached));

                    data = nameof(isCached);
                }
                else
                {
                    data = key;
                }

                cache.Add(key, data);
            }

            cache.Log();
        }
    }
}