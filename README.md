# LambdaCaptureContext

Show different uses of delegates and their performance.

Results om my box:

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2605)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  ShortRun : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

## DemoDelegateNoParams

| Method               | Mean      | Error     | StdDev    | Median    | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|----------:|
| Normal_Call          | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |         - |
| Lambda_Only          | 0.4776 ns | 0.2310 ns | 0.0127 ns | 0.4780 ns |         - |
| Lambda_Static_Only   | 0.4932 ns | 0.0441 ns | 0.0024 ns | 0.4931 ns |         - |
| Method_Group         | 3.4884 ns | 0.4519 ns | 0.0248 ns | 3.4753 ns |         - |
| Lambda_Cached        | 0.0608 ns | 1.9213 ns | 0.1053 ns | 0.0000 ns |         - |
| Lambda_Static_Cached | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |         - |
| Method_Group_Cached  | 3.5296 ns | 0.0158 ns | 0.0009 ns | 3.5298 ns |         - |

## DemoDelegateParams

| Method                                   | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| Normal_Call                              |  0.0290 ns | 0.0152 ns | 0.0008 ns | 0.002 |    0.00 |      - |         - |        0.00 |
| Lambda_With_IntParam                     |  0.4846 ns | 0.0783 ns | 0.0043 ns | 0.035 |    0.00 |      - |         - |        0.00 |
| Lambda_Static_With_IntParam              |  0.4914 ns | 0.4800 ns | 0.0263 ns | 0.035 |    0.00 |      - |         - |        0.00 |
| Lambda_With_CastedObjectParam            |  5.1920 ns | 1.1934 ns | 0.0654 ns | 0.374 |    0.01 | 0.0019 |      24 B |        0.27 |
| Lambda_With_CastedObjectParam_Static     |  5.5246 ns | 1.0141 ns | 0.0556 ns | 0.398 |    0.01 | 0.0019 |      24 B |        0.27 |
| Lambda_Captured_Local_Var                | 13.8754 ns | 5.2843 ns | 0.2897 ns | 1.000 |    0.03 | 0.0070 |      88 B |        1.00 |
| Method_Group_Captured_Local_Function_Var | 13.8377 ns | 9.7563 ns | 0.5348 ns | 0.998 |    0.04 | 0.0070 |      88 B |        1.00 |
| Lambda_Captured_Class_Var                |  8.7491 ns | 2.2914 ns | 0.1256 ns | 0.631 |    0.01 | 0.0051 |      64 B |        0.73 |
| Method_Group_Captured_Class_Var          |  8.9368 ns | 2.4415 ns | 0.1338 ns | 0.644 |    0.01 | 0.0051 |      64 B |        0.73 |
| Lambda_Captured_Local_Var_Cached         |  2.2972 ns | 0.2181 ns | 0.0120 ns | 0.166 |    0.00 |      - |         - |        0.00 |
| Lambda_Static_Captured_Local_Var_Cached  |  2.3095 ns | 0.2374 ns | 0.0130 ns | 0.166 |    0.00 |      - |         - |        0.00 |
| Method_Group_Captured_Local_Var_Cached   |  3.1256 ns | 0.6711 ns | 0.0368 ns | 0.225 |    0.00 |      - |         - |        0.00 |

## MethodGroupVsLambda

| Method              | Mean       | Error      | StdDev    | Gen0   | Allocated |
|-------------------- |-----------:|-----------:|----------:|-------:|----------:|
| Lambda_Capture      |  95.850 ns | 12.5280 ns | 0.6867 ns | 0.0528 |     664 B |
| MethodGroup_Capture |  98.024 ns | 30.3881 ns | 1.6657 ns | 0.0528 |     664 B |
| Lambda              |   8.947 ns |  0.1536 ns | 0.0084 ns |      - |         - |
| MethodGroup         | 117.119 ns | 12.6363 ns | 0.6926 ns | 0.0508 |     640 B |
| MethodGroup_Static  |  34.684 ns |  0.8348 ns | 0.0458 ns |      - |         - |
