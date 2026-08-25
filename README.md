# BenchmarkDotNet fundamentals

// Intro here.

---

## Table of Contents

---

## Setup

---

## Explanation

### Compilation flow

```mermaid
graph LR
	A[Source] -->|Roslyn| B[IL]
	B -->|JIT| C[Native]
	C --> D[CPU]
style D fill:yellow
```

**Roslyn (recipe writing)**: 

The recipe writer (`Roslyn` compiler) turns C# source code into a universal recipe (Intermediate language `IL`). 

The recipe can be baked in any kitchen (Windows, Linux, etc.).

The recipe is stored in a box (`assembly`):

- if the box is marked `.exe`, its ingredients are ready to bake (`Main()`).

- if the box is marked `.dll`, its ingredients are not ready to bake until referenced by an .exe.

**JIT (recipe baking)**:

The kitchen manager (`runtime`) hires the baker (`Just-In-Time` compiler) only when an order comes in (`lazy`).

The baker bakes the recipe using available equipment (`CPU specs`),

Serves it to the customer (`machine code`),

And keeps a copy of cake in the kitchen for future orders (`cache`).

### The four sources of runtime noise

The baker's performance can be affected by a noisy kitchen.

BenchmarkDotNet isolates the noise to reflect the performance of the recipe.

|Source|Description|Solution|
|-----|-----------|--------|
|**JIT**|The first bake takes time. A first bake contains significant compilation time.|Baking the recipe multiple times (`warmup`) and using the cached cake as control.|
|**GC**|Baking produces waste. When the garbage is collected, time is taken to clean up.|Forcing a cleanup between baking batches and mathematically subtracting the cleaning time from the result.|
|**ThreadPool**|Recipes that use threads (parallel tasks) need waiters to serve tables. Waiters can either idle (`overhead`) or tire (`starvation`), slowing down service.|Baking the recipe multiple time so that the waitstaff is stable (`steady state`).|
|**Optimizations**|The baker may skip steps (Dead Code Elimination) if the cake isn't eaten.|Returning the result so BenchmarkDotNet can "eat" it (consume it), forcing the baker to cook.|

## Tutorials

### Stage 1 - First benchmark

`[Benchmark]` marks the specific recipe (`method`) to be measured.

`BenchmarkRunner.Run<T>()` is the runner of the operation.

### The two kitchens workflow (sandboxing)

The runner uses two kitchens (`processes`) to bake the recipe.

Kitchen 1 (`in-process`) is used for **writing** (`compilation`). No cooking happens here.

Kitchen 2 (`out-of-process`) is used for **baking** (`execution`) without any noise from the writing process.

In a same run, each recipe is baked in a separate out-of-process kitchen.

```csharp
// src/StringConcatBenchmark.cs

private const int Iterations = 100;

[Benchmark]
public string Concat()
{
    string result = string.Empty;
    for (int i = 0; i < Iterations; i++)
    {
        result += "x";
    }

    return result;
}

[Benchmark]
public string StringBuilderAppend()
{
    StringBuilder builder = new StringBuilder();
    for (int i = 0; i < Iterations; i++)
    {
        builder.Append("x");
    }

    return builder.ToString();
}
```

```csharp
// src/Program.cs

public static void Main(string[] args)
{
    BenchmarkRunner.Run<StringConcatBenchmarks>();
}
```

```terminal
// Strings are immutable, so concatenation creates a new string each time. 
// StringBuilder is mutable, so it modifies the same object.

| Method              | Mean     | Error    | StdDev   |
|-------------------- |---------:|---------:|---------:|
| Concat              | 815.3 ns | 16.24 ns | 24.81 ns |
| StringBuilderAppend | 146.0 ns |  2.86 ns |  6.28 ns |
```