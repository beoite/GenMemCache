using System.Linq;

namespace GenMemCacheApp
{
    public static class Utility
    {
        // random string of length
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[System.Random.Shared.Next(s.Length)]).ToArray());
        }
    }
}
