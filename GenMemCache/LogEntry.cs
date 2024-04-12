namespace GenMemCache
{
    public class LogEntry
    {
        public ulong Id { get; set; } = 0;

        public long Elapsed { get; set; } = -1;

        public string Text { get; set; } = string.Empty;

        public override string ToString()
        {
            return Id + "\t\t" + Elapsed + "(ms)" + "\t\t" + Text;
        }
    }
}
