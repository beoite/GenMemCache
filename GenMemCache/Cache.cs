namespace GenMemCache
{
    public class Cache<K, V> where K : notnull where V : notnull
    {
        // https://csharpindepth.com/articles/Singleton
        private static readonly Lazy<Cache<K, V>> lazy = new Lazy<Cache<K, V>>(() => new Cache<K, V>());

        private System.Collections.Generic.Dictionary<K, V> dictionary = new();

        private System.Collections.Generic.LinkedList<K> linkedList = new();

        public static Cache<K, V> Instance { get { return lazy.Value; } }

        public int Capacity { get; set; } = 20;

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

            if (dictionary.Count >= Capacity)
            {
                Evict();
            }

            bool containsKey = dictionary.ContainsKey(key);

            if (containsKey == true)
            {
                dictionary[key] = data;
            }
            else
            {
                dictionary.Add(key, data);
            }

            bool isRemoved = linkedList.Remove(key);

            linkedList.AddFirst(key);
        }

        // return an item from the cache, add its key to the head of the list
        public V? Get(K? key)
        {
            if (key is null)
            {
                return default;
            }

            bool containsKey = dictionary.ContainsKey(key);

            bool isRemoved = linkedList.Remove(key);

            if (containsKey == false)
            {
                return default;
            }

            LinkedListNode<K> node = linkedList.AddFirst(key);

            return dictionary[key];
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

            CapacityReachedEventArgs<K> capacityReachedEventArgs = new CapacityReachedEventArgs<K>();

            capacityReachedEventArgs.Message = nameof(Evict) + " " + key.ToString();

            OnCapacityReached(capacityReachedEventArgs);

            bool isRemovedList = linkedList.Remove(key);

            bool isRemovedDictionary = dictionary.Remove(key);
        }

        // clear the cache
        public void Clear()
        {
            dictionary.Clear();

            linkedList.Clear();
        }

        // log the cache to console
        public void Log()
        {
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

            V data = dictionary[key];

            Logger.Log(key?.ToString() + " \t " + data?.ToString());

            if (node.Next is not null)
            {
                Traverse(node.Next);
            }
        }

        // https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1?view=net-8.0
        protected virtual void OnCapacityReached(CapacityReachedEventArgs<K> e)
        {
            if (CapacityReached is null)
            {
                return;
            }

            EventHandler<CapacityReachedEventArgs<K>> handler = CapacityReached;

            if (handler is not null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<CapacityReachedEventArgs<K>>? CapacityReached;
    }
}
