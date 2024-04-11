namespace GenMemCache
{
    internal class CacheTest
    {
        public static void Run()
        {
            Logger.Log(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "." + System.Reflection.MethodBase.GetCurrentMethod()?.Name);

            IntInt();

            UlongString();

            GuidString();

            StringString();

            CapacityTest();
        }

        // Cache<int, int>,  adds some keys and data then prints the contents, along with a test of the Get method
        private static void IntInt()
        {
            for (int i = 0; i < 10; i++)
            {
                int data = System.Random.Shared.Next(100);

                Cache<int, int>.Instance.Add(i, data);
            }

            Cache<int, int>.Instance.Add(111, 0);

            Cache<int, int>.Instance.Log();

            Logger.Log("key 4 : " + Cache<int, int>.Instance.Get(4).ToString());
        }

        // Cache<ulong, string>,  adds some keys and data then prints the contents, along with a test of the Get method
        private static void UlongString()
        {
            for (ulong i = 0; i < 10; i++)
            {
                string data = RandomString(6);

                Cache<ulong, string>.Instance.Add(i, data);
            }

            Cache<ulong, string>.Instance.Add(111, null);

            Cache<ulong, string>.Instance.Log();

            Logger.Log("key 4 : " + Cache<ulong, string>.Instance.Get(4)?.ToString());
        }

        // Cache<Guid, string>,  adds some keys and data then prints the contents, along with a test of the Get method
        private static void GuidString()
        {
            Guid test = Guid.NewGuid();

            for (ulong i = 0; i < 10; i++)
            {
                Guid key = Guid.NewGuid();

                string data = RandomString(6);

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

        // Cache<string, string>,  adds some keys and data then prints the contents, along with a test of the Get method
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

        // test capacity and eviction 
        private static void CapacityTest()
        {
            Cache<uint, string>.Instance.Clear();

            for (uint i = 0; i < 1000; i++)
            {
                string data = RandomString(6);

                Cache<uint, string>.Instance.Add(i, data);
            }

            Cache<uint, string>.Instance.Log();
        }

        // random string of length
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[System.Random.Shared.Next(s.Length)]).ToArray());
        }
    }
}
