namespace GenMemCache
{
    public class Cache<K, V> : Logger where K : notnull where V : notnull
    {
        // https://csharpindepth.com/articles/Singleton

        private const System.Threading.LazyThreadSafetyMode threadSafetyMode = System.Threading.LazyThreadSafetyMode.ExecutionAndPublication;

        private static readonly System.Lazy<Cache<K, V>> lazy = new System.Lazy<Cache<K, V>>(() => new Cache<K, V>(), threadSafetyMode);

        private System.Collections.Concurrent.ConcurrentDictionary<K, V> dictionary = new System.Collections.Concurrent.ConcurrentDictionary<K, V>();

        private System.Collections.Generic.LinkedList<K> linkedList = new System.Collections.Generic.LinkedList<K>();

        private readonly object linkedListLock = new object();

        public static Cache<K, V> Instance { get { return lazy.Value; } }

        public int Capacity { get; set; } = 10;

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

            while (dictionary.Count >= Capacity)
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
                bool isAdded = dictionary.TryAdd(key, data);
            }

            lock (linkedListLock)
            {
                bool isRemovedList = linkedList.Remove(key);

                System.Collections.Generic.LinkedListNode<K> node = linkedList.AddFirst(key);
            }
        }

        // return an item from the cache, add its key to the head of the list
        public V? Get(K? key)
        {
            if (key is null)
            {
                return default;
            }

            bool containsKey = dictionary.ContainsKey(key);

            if (containsKey == false)
            {
                return default;
            }

            lock (linkedListLock)
            {
                bool isRemovedList = linkedList.Remove(key);

                System.Collections.Generic.LinkedListNode<K> node = linkedList.AddFirst(key);
            }

            return dictionary[key];
        }

        // The cache should implement the ‘least recently used’ approach when selecting which item to evict.
        private void Evict()
        {
            lock (linkedListLock)
            {
                System.Collections.Generic.LinkedListNode<K>? node = linkedList.Last;

                if (node is null)
                {
                    return;
                }

                K key = node.Value;

                bool isRemovedList = linkedList.Remove(key);

                V? outItem = default;

                bool isRemovedDictionary = dictionary.TryRemove(key, out outItem);

                CapacityReachedEventArgs capacityReachedEventArgs = new CapacityReachedEventArgs();

                capacityReachedEventArgs.Message = nameof(Evict) + " " + key.ToString();

                OnCapacityReached(capacityReachedEventArgs);
            }
        }

        // clear the cache
        public new void Clear()
        {
            base.Clear();

            dictionary.Clear();

            linkedList.Clear();
        }

        // log the cache
        public void Log()
        {
            base.Clear();

            RecursiveLog(linkedList.First);
        }

        // recursively traverse nodes
        private void RecursiveLog(System.Collections.Generic.LinkedListNode<K>? node)
        {
            if (node is null)
            {
                return;
            }

            K key = node.Value;

            bool containsKey = dictionary.ContainsKey(key);

            if (containsKey == false)
            {
                return;
            }

            V data = dictionary[key];

            Log(key?.ToString() + " \t " + data?.ToString());

            if (node.Next is not null)
            {
                RecursiveLog(node.Next);
            }
        }

        // https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1?view=net-8.0
        protected virtual void OnCapacityReached(CapacityReachedEventArgs e)
        {
            if (CapacityReached is null)
            {
                return;
            }

            System.EventHandler<CapacityReachedEventArgs> handler = CapacityReached;

            if (handler is not null)
            {
                handler(this, e);
            }
        }

        public event System.EventHandler<CapacityReachedEventArgs>? CapacityReached;
    }
}
