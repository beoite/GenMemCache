namespace GenMemCache
{
    public class LogEntry
    {
        public ulong Id { get; set; } = 0;

        public long Elapsed { get; set; } = -1;

        public string Text { get; set; } = string.Empty;
    }
}
