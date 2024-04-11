namespace GenMemCache
{
    public class Cache<K, V> where K : notnull where V : notnull
    {
        // https://csharpindepth.com/articles/Singleton
        private static readonly Lazy<Cache<K, V>> lazy = new Lazy<Cache<K, V>>(() => new Cache<K, V>());

        private System.Collections.Generic.Dictionary<K, V> cache = new();

        private System.Collections.Generic.LinkedList<K> linkedList = new();

        public static Cache<K, V> Instance { get { return lazy.Value; } }

        public uint Capacity { get; set; } = 20;

        private Cache()
        {
        }

        // add an item to the cache
        public void Add(K key, V data)
        {
            if (key is null || data is null)
            {
                return;
            }

            if (cache.Count >= Capacity)
            {
                Evict();
            }

            linkedList.AddFirst(key);

            cache.Add(key, data);
        }

        // return an item from the cache
        public V? Get(K? key)
        {
            if (key is null)
            {
                return default;
            }

            linkedList.Remove(key);

            linkedList.AddFirst(key);

            return cache[key];
        }

        // The cache should implement the ‘least recently used’ approach when selecting which item to evict.
        private void Evict()
        {
            LinkedListNode<K>? node = linkedList.Last;

            if (node is null)
            {
                return;
            }

            K key = node.Value;

            linkedList.Remove(key);

            cache.Remove(key);
        }

        // log the cache to console
        public void Log()
        {
            Logger.Log(System.Environment.NewLine);
            Logger.Log(nameof(Log) + " <" + typeof(K).ToString() + ", " + typeof(V).ToString() + ">");

            Traverse(linkedList.First);
        }

        // recursively traverse nodes
        private void Traverse(LinkedListNode<K>? node)
        {
            if (node is null)
            {
                return;
            }

            K key = node.Value;

            V data = cache[key];

            Logger.Log(key?.ToString() + " \t " + data?.ToString());

            if (node.Next is not null)
            {
                Traverse(node.Next);
            }
        }

        // clear the cache
        public void Clear()
        {
            cache.Clear();

            linkedList.Clear();
        }
    }
}
