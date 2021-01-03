# Construction Line code challenge

The code challenge consists in the implementation of a simple search engine for shirts.

## What does it include?

The search engine class includes two different search methods where the first one uses predicates only to  find the wanted shirts like the following code to go through all shirts with a specific size and color

```

    foreach (var shirt in _shirts.FindAll(s => s.Size.Name.Equals(size.Name) && s.Color.Name.Equals(color.Name)))
    {
        .
        .
        .
    }

```

Where the second search method is based mostly on for statement lists as simple arrays where no predicate condition is involved. Here is the last example according to this concept:

```

    for (int shirtIndex = 0; shirtIndex < options.Sizes.Count; shirtIndex++)
    {
        foreach (var shirt in _shirts)
        {
            if (options.Sizes[shirtIndex].Name.Equals(shirt.Size.Name))
            {
                .
                .
                .
            }
        }
    }

```
## Test issues
Obviously, there is a bug in the test, which accepts only few searching cases. 
For this reason and in order to cover all searching cases, two ammendements have been made on the following assessment methods:

```
    protected static void AssertSizeCounts(...)
    {
    
    }
    
    protected static void AssertColorCounts(...)
    {
    
    }

```

## Execution Time

The recorded time during the performance test was 95 ms.
