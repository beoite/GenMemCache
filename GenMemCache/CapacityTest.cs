namespace GenMemCache
{
    internal class CapacityTest
    {
        public CapacityTest()
        {
            Logger.Log(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "." + System.Reflection.MethodBase.GetCurrentMethod()?.Name);

            Cache<int, string>.Instance.CapacityReached += CapacityReachedHandler;

            Cache<int, string>.Instance.Clear();

            for (int i = 0; i < Cache<int, string>.Instance.Capacity * 2; i++)
            {
                string data = Utility.RandomString(6);

                Cache<int, string>.Instance.Add(i, data);
            }

            Cache<int, string>.Instance.Log();
        }

        ~CapacityTest()
        {
            Logger.Log(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "." + System.Reflection.MethodBase.GetCurrentMethod()?.Name);

            Cache<int, string>.Instance.CapacityReached -= CapacityReachedHandler;
        }

        private void CapacityReachedHandler(object? sender, CapacityReachedEventArgs<int> e)
        {
            Logger.Log(nameof(CapacityReachedHandler) + " " + e.ToString());
        }
    }
}
