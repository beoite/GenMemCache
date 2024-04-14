namespace GenMemCache
{
    public class Logger
    {
        public System.Diagnostics.Stopwatch Watch { get; set; } = System.Diagnostics.Stopwatch.StartNew();

        public System.Collections.Generic.List<LogEntry> Entries { get; set; } = new System.Collections.Generic.List<LogEntry>();

        public ulong Id { get; set; } = 0;


        // Usage : Logger.Log(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "." + System.Reflection.MethodBase.GetCurrentMethod()?.Name);
        public void Log(string text)
        {
            long elapsed = Watch.ElapsedMilliseconds;

            LogEntry logEntry = new LogEntry();
            logEntry.Id = ++Id;
            logEntry.Elapsed = elapsed;
            logEntry.Text = text;

            Entries.Add(logEntry);

            Watch.Restart();
        }

        public void Clear()
        {
            Entries.Clear();

            Watch.Reset();

            Id = 0;
        }

        public void ConsoleWriteLine()
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            for (int i = 0; i < Entries.Count; i++)
            {
                LogEntry logEntry = Entries[i];

                stringBuilder.Append(logEntry.Text + System.Environment.NewLine);
            }

            System.Console.WriteLine(stringBuilder.ToString());
        }
    }
}
