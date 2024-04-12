namespace GenMemCacheApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // basic tests

            TestCache.Run();

            // test capacity and eviction 

            TestCapacity testCapacity = new TestCapacity();

            // test threading

            TestThreading testThreading = new TestThreading();
        }
    }
}