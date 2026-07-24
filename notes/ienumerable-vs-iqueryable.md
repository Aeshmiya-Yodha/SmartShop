# IEnumerable vs IQueryable (my simple notes)

These are my own notes in simple language so I don't cram it again later.

## One line each

- IEnumerable = "I can give items one by one." (reading/iterating)
- IQueryable = "I am a query still being built." (becomes SQL before data comes)

---

## IEnumerable (the real feel)

IEnumerable is an interface. Its ONLY main job is:

> "I can give you an object (enumerator) that walks through items one by one."

Important points I understood:

1. It does NOT store data itself.
2. The real data lives in the actual object behind it (List, array, DbSet, generated sequence).
3. The variable is just a reference of interface type pointing to a real object on the heap.
4. `foreach` works because the object gives an enumerator (GetEnumerator -> MoveNext -> Current).

### Example

```csharp
IEnumerable<Product> AllProducts = _context.Products;
```

- `AllProducts` does NOT hold products.
- It points to `_context.Products` (a DbSet object that represents the Products table).
- Data is NOT loaded yet at this line.

### What happens on filtering

```csharp
Products = AllProducts.Where(p => p.ProductName == ProductName).ToList();
```

Because the type is IEnumerable:

- `.Where(...)` becomes `Enumerable.Where` = C# side filtering.
- Nothing runs until `.ToList()` (this is the trigger / terminal action).
- At `.ToList()`, items are pulled one by one from the source, condition is checked IN C# MEMORY, matching ones go into a new list.

Flow:

```text
DB gives products -> app gets items -> C# filters -> List stores result
```

My mental model (correct for my level):

> I have an IEnumerable reference to a data source. When I do Where(...).ToList(), it pulls elements one by one, checks the condition in C#, and appends matching ones into a list. All fetching + processing happens only when a terminal action (ToList) runs, and it happens in the app memory.

---

## IQueryable (the real feel)

IQueryable means:

> "I am not data yet. I am a query being built."

### Example

```csharp
IQueryable<Product> AllProducts = _context.Products;

AllProducts = AllProducts.Where(p => p.ProductName == ProductName); // just adds condition

List<Product> Products = await AllProducts.ToListAsync(); // NOW query runs in DB
```

Because the type is IQueryable:

- `.Where(...)` becomes `Queryable.Where` = it records the condition as query info.
- EF Core converts it to SQL like `WHERE ProductName = ...`.
- DB filters first, then only the needed rows come back.

Flow:

```text
C# builds SQL -> DB filters -> only result rows come -> List stores result
```

---

## The main magic (why same code behaves differently)

Same looking line:

```csharp
.Where(p => p.ProductName == ProductName)
```

But the TYPE decides which Where is used:

```text
IEnumerable -> Enumerable.Where -> filter in C# memory
IQueryable  -> Queryable.Where  -> filter becomes SQL in DB
```

So my logic was fine earlier, only the datatype choice was wrong.

---

## Why IQueryable is faster for DB (shop example)

Shop has 1,00,000 products, I want only "Shoes".

- IEnumerable way: bring lots of rows to app, then filter in C# (more data, more RAM, more CPU).
- IQueryable way: tell DB "give only shoes", DB returns ~few rows.

Less data movement = faster.

---

## Practical rule for SmartShop

```csharp
IQueryable<Product> query = _context.Products;

// add filters here using if/else
// query = query.Where(...);

List<Product> products = await query.ToListAsync(); // materialize ONCE at the end
```

- Keep it IQueryable while talking to DB.
- Add filters step by step.
- Call ToListAsync() only once at the end.
- After ToList/ToListAsync, data is in memory -> further Where(...) is IEnumerable (C# side).

---

## Enough-for-my-level checklist

- [x] IEnumerable = read items one by one, does not store data.
- [x] Real data is behind it (List, array, DbSet...).
- [x] IEnumerable + EF = filtering in C# memory.
- [x] IQueryable = builds SQL first, filtering in DB.
- [x] Rule: use IQueryable, filter, then ToListAsync() once.

Not needed right now (later stuff): enumerator internals, expression trees, providers, delegates, covariance.
