namespace GenMemCache
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger.Log(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "." + System.Reflection.MethodBase.GetCurrentMethod()?.Name);

            CacheTest.Run();
        }
    }
}