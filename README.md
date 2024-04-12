<p>
    A generic in memory cache
</p>

<p>
    Usage:
</p>

```
    GenMemCache.Cache<string, string> cache = GenMemCache.Cache<string, string>.Instance;

    cache.Clear();

    // won't work
    cache.Add(null, "");

    // won't work
    cache.Add("", null);

    cache.Add("1", "h");

    cache.Add("2", "e");

    cache.Add("3", "l");

    cache.Add("4", "l");

    cache.Add("5", "o");

    cache.Add("duplicate", "1");

    cache.Add("duplicate", "2");

    // this is the duplicate saved
    cache.Add("duplicate", "2");

    cache.Log();
```
<p>
    Prints the following:
</p>

```
1               0(ms)           Log <System.String, System.String>
2               0(ms)           duplicate        2
3               0(ms)           5        o
4               0(ms)           4        l
5               0(ms)           3        l
6               0(ms)           2        e
7               0(ms)           1        h
```