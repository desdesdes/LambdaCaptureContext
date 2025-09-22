# LambdaCaptureContext

Show different uses of delegates and their performance.

Results om my box:

BenchmarkDotNet v0.15.3, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core Ultra 9 185H 3.10GHz, 1 CPU, 22 logical and 16 physical cores
.NET SDK 10.0.100-rc.1.25451.107
  [Host]   : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  ShortRun : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Job=ShortRun  Runtime=.NET 9.0  Toolchain=net9.0  
IterationCount=3  LaunchCount=1  WarmupCount=3  

## DemoDelegateNoParams

| Method               | Mean      | Error     | StdDev    | Median    | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|----------:|
| Normal_Call          | 0.0907 ns | 0.9580 ns | 0.0525 ns | 0.1142 ns |         - |
| Lambda_Only          | 0.1572 ns | 1.4728 ns | 0.0807 ns | 0.1839 ns |         - |
| Lambda_Static_Only   | 0.2395 ns | 1.2921 ns | 0.0708 ns | 0.2750 ns |         - |
| Method_Group         | 1.8028 ns | 0.8394 ns | 0.0460 ns | 1.8230 ns |         - |
| Lambda_Cached        | 0.0751 ns | 1.1844 ns | 0.0649 ns | 0.1120 ns |         - |
| Lambda_Static_Cached | 0.1648 ns | 1.4810 ns | 0.0812 ns | 0.2046 ns |         - |
| Method_Group_Cached  | 1.6300 ns | 2.1484 ns | 0.1178 ns | 1.6886 ns |         - |

## DemoDelegateParams

| Method                                   | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| Normal_Call                              |  0.0333 ns | 0.5709 ns | 0.0313 ns | 0.003 |    0.00 |      - |         - |        0.00 |
| Lambda_With_IntParam                     |  0.1852 ns | 0.7635 ns | 0.0418 ns | 0.016 |    0.00 |      - |         - |        0.00 |
| Lambda_Static_With_IntParam              |  0.2593 ns | 0.2558 ns | 0.0140 ns | 0.023 |    0.00 |      - |         - |        0.00 |
| Lambda_With_CastedObjectParam            |  4.4331 ns | 2.4806 ns | 0.1360 ns | 0.392 |    0.01 | 0.0005 |      24 B |        0.27 |
| Lambda_With_CastedObjectParam_Static     |  4.4693 ns | 1.9662 ns | 0.1078 ns | 0.395 |    0.01 | 0.0005 |      24 B |        0.27 |
| Lambda_Captured_Local_Var                | 11.3222 ns | 5.4347 ns | 0.2979 ns | 1.000 |    0.03 | 0.0017 |      88 B |        1.00 |
| Method_Group_Captured_Local_Function_Var | 11.4567 ns | 1.0150 ns | 0.0556 ns | 1.012 |    0.02 | 0.0017 |      88 B |        1.00 |
| Lambda_Captured_Class_Var                |  8.0925 ns | 4.2138 ns | 0.2310 ns | 0.715 |    0.02 | 0.0012 |      64 B |        0.73 |
| Method_Group_Captured_Class_Var          |  7.7699 ns | 0.8866 ns | 0.0486 ns | 0.687 |    0.02 | 0.0012 |      64 B |        0.73 |
| Lambda_Captured_Local_Var_Cached         |  0.8251 ns | 3.8907 ns | 0.2133 ns | 0.073 |    0.02 |      - |         - |        0.00 |
| Lambda_Static_Captured_Local_Var_Cached  |  0.7473 ns | 2.3552 ns | 0.1291 ns | 0.066 |    0.01 |      - |         - |        0.00 |
| Method_Group_Captured_Local_Var_Cached   |  1.7267 ns | 0.1400 ns | 0.0077 ns | 0.153 |    0.00 |      - |         - |        0.00 |

## MethodGroupVsLambda

| Method              | Mean       | Error     | StdDev    | Gen0   | Allocated |
|-------------------- |-----------:|----------:|----------:|-------:|----------:|
| Lambda_Capture      | 119.494 ns | 49.462 ns | 2.7112 ns | 0.0126 |     664 B |
| MethodGroup_Capture | 119.798 ns | 96.808 ns | 5.3064 ns | 0.0126 |     664 B |
| Lambda              |   4.222 ns |  5.883 ns | 0.3225 ns |      - |         - |
| MethodGroup         | 124.558 ns | 39.948 ns | 2.1897 ns | 0.0122 |     640 B |
| MethodGroup_Static  |  18.368 ns | 17.808 ns | 0.9761 ns |      - |         - |
