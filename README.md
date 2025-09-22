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
| Normal_Call          | 0.0621 ns | 0.9824 ns | 0.0538 ns | 0.0904 ns |         - |
| Lambda_Only          | 0.2722 ns | 1.0820 ns | 0.0593 ns | 0.3042 ns |         - |
| Lambda_Static_Only   | 0.1412 ns | 0.7555 ns | 0.0414 ns | 0.1243 ns |         - |
| Method_Group         | 1.7531 ns | 2.4253 ns | 0.1329 ns | 1.8261 ns |         - |
| Lambda_Cached        | 0.1621 ns | 1.2862 ns | 0.0705 ns | 0.1957 ns |         - |
| Lambda_Static_Cached | 0.0774 ns | 1.2237 ns | 0.0671 ns | 0.1154 ns |         - |
| Method_Group_Cached  | 1.7349 ns | 1.8674 ns | 0.1024 ns | 1.7807 ns |         - |

## DemoDelegateParams

| Method                                   | Mean       | Error     | StdDev    | Median     | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------------------------------------- |-----------:|----------:|----------:|-----------:|------:|--------:|-------:|----------:|------------:|
| Normal_Call                              |  0.0038 ns | 0.0634 ns | 0.0035 ns |  0.0046 ns | 0.000 |    0.00 |      - |         - |        0.00 |
| Lambda_With_IntParam                     |  0.1955 ns | 0.1306 ns | 0.0072 ns |  0.1963 ns | 0.016 |    0.00 |      - |         - |        0.00 |
| Lambda_Static_With_IntParam              |  0.2611 ns | 0.8885 ns | 0.0487 ns |  0.2862 ns | 0.021 |    0.00 |      - |         - |        0.00 |
| Lambda_With_CastedObjectParam            |  4.5767 ns | 0.8872 ns | 0.0486 ns |  4.5623 ns | 0.372 |    0.01 | 0.0005 |      24 B |        0.27 |
| Lambda_With_CastedObjectParam_Static     |  4.6458 ns | 0.5439 ns | 0.0298 ns |  4.6463 ns | 0.377 |    0.01 | 0.0005 |      24 B |        0.27 |
| Lambda_Captured_Local_Var                | 12.3221 ns | 5.6202 ns | 0.3081 ns | 12.1707 ns | 1.000 |    0.03 | 0.0017 |      88 B |        1.00 |
| Method_Group_Captured_Local_Function_Var | 12.3249 ns | 0.6571 ns | 0.0360 ns | 12.3331 ns | 1.001 |    0.02 | 0.0017 |      88 B |        1.00 |
| Lambda_Captured_Class_Var                |  8.4671 ns | 2.7042 ns | 0.1482 ns |  8.4058 ns | 0.687 |    0.02 | 0.0012 |      64 B |        0.73 |
| Method_Group_Captured_Class_Var          |  8.7003 ns | 5.6162 ns | 0.3078 ns |  8.5483 ns | 0.706 |    0.03 | 0.0012 |      64 B |        0.73 |
| Lambda_Captured_Local_Var_Cached         |  0.7940 ns | 0.2401 ns | 0.0132 ns |  0.7943 ns | 0.064 |    0.00 |      - |         - |        0.00 |
| Lambda_Static_Captured_Local_Var_Cached  |  0.7276 ns | 1.6347 ns | 0.0896 ns |  0.7729 ns | 0.059 |    0.01 |      - |         - |        0.00 |
| Method_Group_Captured_Local_Var_Cached   |  1.7146 ns | 0.1324 ns | 0.0073 ns |  1.7163 ns | 0.139 |    0.00 |      - |         - |        0.00 |

## MethodGroupVsLambda

| Method              | Mean       | Error      | StdDev    | Gen0   | Allocated |
|-------------------- |-----------:|-----------:|----------:|-------:|----------:|
| Lambda_Capture      | 127.420 ns | 45.5358 ns | 2.4960 ns | 0.0126 |     664 B |
| MethodGroup_Capture | 121.138 ns | 43.2278 ns | 2.3695 ns | 0.0126 |     664 B |
| Lambda              |   3.848 ns |  0.4069 ns | 0.0223 ns |      - |         - |
| Lambda_Static       |   3.865 ns |  0.3426 ns | 0.0188 ns |      - |         - |
| MethodGroup         |  94.299 ns | 24.1053 ns | 1.3213 ns | 0.0123 |     640 B |
| MethodGroup_Static  |  17.323 ns |  0.8385 ns | 0.0460 ns |      - |         - |
