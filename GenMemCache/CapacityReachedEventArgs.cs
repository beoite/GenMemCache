namespace GenMemCache
{
    public class CapacityReachedEventArgs<K> : System.EventArgs
    {
        public string Message { get; set; } = string.Empty;
    }
}
