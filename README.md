# LambdaCaptureContext

Show different uses of delegates and their performance.

Results om my box:

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4037/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100-preview.7.24407.12
  [Host]   : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  ShortRun : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=ShortRun  IterationCount=3  LaunchCount=1
WarmupCount=3

## DemoDelegateNoParams

| Method               | Mean      | Error     | StdDev    | Median    | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|----------:|
| Normal_Call          | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |         - |
| Lambda_Only          | 0.0258 ns | 0.6627 ns | 0.0363 ns | 0.0058 ns |         - |
| Lambda_Static_Only   | 0.0326 ns | 0.9064 ns | 0.0497 ns | 0.0056 ns |         - |
| Method_Group         | 3.0590 ns | 0.0088 ns | 0.0005 ns | 3.0591 ns |         - |
| Lambda_Cached        | 0.0040 ns | 0.0154 ns | 0.0008 ns | 0.0036 ns |         - |
| Lambda_Static_Cached | 0.0033 ns | 0.0138 ns | 0.0008 ns | 0.0037 ns |         - |
| Method_Group_Cached  | 2.6237 ns | 0.0470 ns | 0.0026 ns | 2.6231 ns |         - |

## DemoDelegateParams

| Method                                   | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| Normal_Call                              |  0.0000 ns | 0.0000 ns | 0.0000 ns | 0.000 |    0.00 |      - |         - |        0.00 |
| Lambda_With_IntParam                     |  0.4411 ns | 0.0409 ns | 0.0022 ns | 0.039 |    0.00 |      - |         - |        0.00 |
| Lambda_Static_With_IntParam              |  0.4639 ns | 0.2869 ns | 0.0157 ns | 0.041 |    0.00 |      - |         - |        0.00 |
| Lambda_With_CastedObjectParam            |  3.4811 ns | 0.5900 ns | 0.0323 ns | 0.308 |    0.00 | 0.0019 |      24 B |        0.27 |
| Lambda_With_CastedObjectParam_Static     |  3.6025 ns | 2.8937 ns | 0.1586 ns | 0.319 |    0.01 | 0.0019 |      24 B |        0.27 |
| Lambda_Captured_Local_Var                | 11.2909 ns | 2.7575 ns | 0.1511 ns | 1.000 |    0.02 | 0.0070 |      88 B |        1.00 |
| Method_Group_Captured_Local_Function_Var | 11.5081 ns | 1.8609 ns | 0.1020 ns | 1.019 |    0.01 | 0.0070 |      88 B |        1.00 |
| Lambda_Captured_Class_Var                |  7.2945 ns | 1.5021 ns | 0.0823 ns | 0.646 |    0.01 | 0.0051 |      64 B |        0.73 |
| Method_Group_Captured_Class_Var          |  7.4752 ns | 5.4957 ns | 0.3012 ns | 0.662 |    0.02 | 0.0051 |      64 B |        0.73 |
| Lambda_Captured_Local_Var_Cached         |  2.2522 ns | 0.3297 ns | 0.0181 ns | 0.199 |    0.00 |      - |         - |        0.00 |
| Lambda_Static_Captured_Local_Var_Cached  |  2.2864 ns | 0.1074 ns | 0.0059 ns | 0.203 |    0.00 |      - |         - |        0.00 |
| Method_Group_Captured_Local_Var_Cached   |  4.0645 ns | 8.8590 ns | 0.4856 ns | 0.360 |    0.04 |      - |         - |        0.00 |

## MethodGroupVsLambda

| Method              | Mean      | Error     | StdDev   | Gen0   | Allocated |
|-------------------- |----------:|----------:|---------:|-------:|----------:|
| Lambda_Capture      | 103.72 ns | 19.663 ns | 1.078 ns | 0.0528 |     664 B |
| MethodGroup_Capture | 100.81 ns | 17.102 ns | 0.937 ns | 0.0528 |     664 B |
| Lambda              |  12.55 ns |  1.189 ns | 0.065 ns |      - |         - |
| MethodGroup         | 107.51 ns | 20.087 ns | 1.101 ns | 0.0509 |     640 B |
| MethodGroup_Static  |  43.42 ns |  4.323 ns | 0.237 ns |      - |         - |