namespace GenMemCache
{
    public static class Logger
    {
        public static System.Diagnostics.Stopwatch Watch { get; set; } = System.Diagnostics.Stopwatch.StartNew();

        public static System.Collections.Generic.List<LogEntry> Entries { get; set; } = new System.Collections.Generic.List<LogEntry>();

        public static ulong Id { get; set; } = 0;


        // Usage : Logger.Log(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "." + System.Reflection.MethodBase.GetCurrentMethod()?.Name);
        public static void Log(string text)
        {
            long elapsed = Watch.ElapsedMilliseconds;

            LogEntry logEntry = new LogEntry();
            logEntry.Id = ++Id;
            logEntry.Elapsed = elapsed;
            logEntry.Text = text;

            Entries.Add(logEntry);

            System.Console.WriteLine(Id + "\t" + elapsed + "(ms)" + "\t\t" + text + "\t");

            Watch.Restart();
        }

        public static void Clear()
        {
            Entries.Clear();

            Watch.Reset();

            Id = 0;
        }
    }
}
