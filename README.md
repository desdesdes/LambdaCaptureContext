# LambdaCaptureContext

Show different uses of delegates and their performance.

Results om my box:

BenchmarkDotNet v0.15.3, Windows 11 (10.0.26200.7623)
Intel Core Ultra 9 185H 3.10GHz, 1 CPU, 22 logical and 16 physical cores
.NET SDK 10.0.102
  [Host]   : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v3
  ShortRun : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v3

Job=ShortRun  Runtime=.NET 10.0  Toolchain=net10.0  
IterationCount=3  LaunchCount=1  WarmupCount=3  

## DemoDelegateNoParams

| Method               | Mean      | Error     | StdDev    | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|
| Normal_Call          | 0.0000 ns | 0.0000 ns | 0.0000 ns |         - |
| Lambda_Only          | 0.0510 ns | 0.0447 ns | 0.0025 ns |         - |
| Lambda_Static_Only   | 0.0730 ns | 0.0311 ns | 0.0017 ns |         - |
| Method_Group         | 1.7265 ns | 0.1411 ns | 0.0077 ns |         - |
| Lambda_Cached        | 0.0788 ns | 0.0218 ns | 0.0012 ns |         - |
| Lambda_Static_Cached | 0.0790 ns | 0.0410 ns | 0.0022 ns |         - |
| Method_Group_Cached  | 1.6916 ns | 0.1543 ns | 0.0085 ns |         - |

## DemoDelegateParams

| Method                                   | Mean      | Error     | StdDev    | Median    | Ratio | Gen0   | Allocated | Alloc Ratio |
|----------------------------------------- |----------:|----------:|----------:|----------:|------:|-------:|----------:|------------:|
| Normal_Call                              | 0.0133 ns | 0.0734 ns | 0.0040 ns | 0.0122 ns | 0.004 |      - |         - |        0.00 |
| Lambda_With_IntParam                     | 0.0676 ns | 0.1076 ns | 0.0059 ns | 0.0655 ns | 0.018 |      - |         - |        0.00 |
| Lambda_Static_With_IntParam              | 0.0600 ns | 0.0598 ns | 0.0033 ns | 0.0599 ns | 0.016 |      - |         - |        0.00 |
| Lambda_With_CastedObjectParam            | 3.8324 ns | 1.1318 ns | 0.0620 ns | 3.8045 ns | 1.031 | 0.0005 |      24 B |        1.00 |
| Lambda_With_CastedObjectParam_Static     | 3.7851 ns | 0.4217 ns | 0.0231 ns | 3.7906 ns | 1.019 | 0.0005 |      24 B |        1.00 |
| Lambda_Captured_Local_Var                | 3.7159 ns | 0.2819 ns | 0.0155 ns | 3.7235 ns | 1.000 | 0.0005 |      24 B |        1.00 |
| Method_Group_Captured_Local_Function_Var | 3.5145 ns | 0.9570 ns | 0.0525 ns | 3.5029 ns | 0.946 | 0.0005 |      24 B |        1.00 |
| Lambda_Captured_Class_Var                | 0.0050 ns | 0.0591 ns | 0.0032 ns | 0.0058 ns | 0.001 |      - |         - |        0.00 |
| Method_Group_Captured_Class_Var          | 0.0070 ns | 0.1259 ns | 0.0069 ns | 0.0037 ns | 0.002 |      - |         - |        0.00 |
| Lambda_Captured_Local_Var_Cached         | 0.6504 ns | 0.0489 ns | 0.0027 ns | 0.6516 ns | 0.175 |      - |         - |        0.00 |
| Lambda_Static_Captured_Local_Var_Cached  | 0.6509 ns | 0.0688 ns | 0.0038 ns | 0.6528 ns | 0.175 |      - |         - |        0.00 |
| Method_Group_Captured_Local_Var_Cached   | 1.4898 ns | 0.0366 ns | 0.0020 ns | 1.4897 ns | 0.401 |      - |         - |        0.00 |

## MethodGroupVsLambda

| Method              | Mean       | Error      | StdDev    | Gen0   | Allocated |
|-------------------- |-----------:|-----------:|----------:|-------:|----------:|
| Lambda_Capture      | 131.864 ns | 86.9297 ns | 4.7649 ns | 0.0126 |     664 B |
| MethodGroup_Capture | 122.385 ns | 56.0916 ns | 3.0746 ns | 0.0126 |     664 B |
| Lambda              |   4.824 ns |  0.3056 ns | 0.0168 ns |      - |         - |
| Lambda_Static       |   4.821 ns |  0.7053 ns | 0.0387 ns |      - |         - |
| MethodGroup         |  95.777 ns | 55.5455 ns | 3.0446 ns | 0.0122 |     640 B |
| MethodGroup_Static  |  17.719 ns |  0.5480 ns | 0.0300 ns |      - |         - |
