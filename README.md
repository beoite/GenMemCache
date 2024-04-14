<p>
    A generic least recently used in memory cache
</p>

<p>
    Usage:
</p>

```
    GenMemCache.Cache<string, string> cache = GenMemCache.Cache<string, string>.Instance;

    cache.Clear();

    cache.Add(null, ""); // won't work

    cache.Add("", null); // won't work

    cache.Add("1", "h");

    cache.Add("2", "e");

    cache.Add("3", "l");

    cache.Add("4", "l");

    cache.Add("5", "o");

    string duplicate = "duplicate";

    cache.Add(duplicate, "1");

    cache.Add(duplicate, "2");

    cache.Add(duplicate, "3");

    string? data = cache.Get(duplicate);

    System.Console.WriteLine(data); // prints 3
    
    //  the cache looks like this
    //  duplicate   3
    //  5           o
    //  4           l
    //  3           l
    //  2           e
    //  1           h
```