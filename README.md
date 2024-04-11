<p>
    A generic in memory cache
</p>

<p>
    Usage
</p>

    ```
    Cache<int, int>.Instance.Add(1, 42);
    Cache<int, int>.Instance.Add(2, 42);
    ```

    ```
    Cache<string, string>.Instance.Add("key1", "data");
    Cache<string, string>.Instance.Add("key2", "data");
    ```