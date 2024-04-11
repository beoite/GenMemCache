namespace GenMemCache
{
    internal class CacheTest
    {
        public static void Run()
        {
            Logger.Log(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "." + System.Reflection.MethodBase.GetCurrentMethod()?.Name);

            // add some keys and data then prints the contents, along with a test of the Get method

            IntInt();

            GuidString();

            StringString();

            // test capacity and eviction 

            CapacityTest capacityTest = new CapacityTest();
        }

        // Cache<int, int>
        private static void IntInt()
        {
            for (int i = 0; i < 101; i++)
            {
                int data = System.Random.Shared.Next(100);

                Cache<int, int>.Instance.Add(i, data);
            }

            Cache<int, int>.Instance.Add(111, 0);

            Cache<int, int>.Instance.Log();

            Logger.Log("key 4 : " + Cache<int, int>.Instance.Get(4).ToString());
        }

        // Cache<Guid, string>
        private static void GuidString()
        {
            Guid test = Guid.NewGuid();

            for (ulong i = 0; i < 10; i++)
            {
                Guid key = Guid.NewGuid();

                string data = Utility.RandomString(6);

                Cache<Guid, string>.Instance.Add(key, data);

                if (i == 4)
                {
                    test = key;
                }
            }

            Cache<Guid, string>.Instance.Add(Guid.NewGuid(), null);

            Cache<Guid, string>.Instance.Log();

            Logger.Log("key " + test + " : " + Cache<Guid, string>.Instance.Get(test)?.ToString());
        }

        // Cache<string, string>
        private static void StringString()
        {
            Cache<string, string>.Instance.Add(null, "");

            Cache<string, string>.Instance.Add("", null);

            Cache<string, string>.Instance.Add("1", "h");

            Cache<string, string>.Instance.Add("2", "e");

            Cache<string, string>.Instance.Add("3", "l");

            Cache<string, string>.Instance.Add("4", "l");

            Cache<string, string>.Instance.Add("5", "o");

            Cache<string, string>.Instance.Log();

            Logger.Log("key 4 : " + Cache<string, string>.Instance.Get("4")?.ToString());
        }
    }
}
