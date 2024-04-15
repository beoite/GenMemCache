namespace GenMemCacheApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tests();

            System.Console.WriteLine("- test metrics (y) ?");

            System.ConsoleKeyInfo readKey = System.Console.ReadKey();

            if (readKey.KeyChar == 'y')
            {
                MainLoop mainLoop = new MainLoop();
            }
        }

        private static void Tests()
        {
            System.Console.WriteLine("- basic tests");

            TestBasic testBasic = new TestBasic();

            System.Console.WriteLine("- test capacity and eviction");

            TestCapacity testCapacity = new TestCapacity();

            System.Console.WriteLine("- test threading");

            TestThreading testThreading = new TestThreading();
        }
    }
}