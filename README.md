# LambdaCaptureContext

Show different uses of delegates and their performance.

Results om my box:

BenchmarkDotNet v0.15.3, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core Ultra 9 185H 3.10GHz, 1 CPU, 22 logical and 16 physical cores
.NET SDK 10.0.100-rc.1.25451.107
  [Host]   : .NET 10.0.0 (10.0.0-rc.1.25451.107, 10.0.25.45207), X64 RyuJIT x86-64-v3
  ShortRun : .NET 10.0.0 (10.0.0-rc.1.25451.107, 10.0.25.45207), X64 RyuJIT x86-64-v3

Job=ShortRun  Runtime=.NET 10.0  Toolchain=net10.0  
IterationCount=3  LaunchCount=1  WarmupCount=3  

## DemoDelegateNoParams

| Method               | Mean      | Error     | StdDev    | Median    | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|----------:|
| Normal_Call          | 0.0195 ns | 0.3509 ns | 0.0192 ns | 0.0200 ns |         - |
| Lambda_Only          | 0.2371 ns | 1.5687 ns | 0.0860 ns | 0.2858 ns |         - |
| Lambda_Static_Only   | 0.1033 ns | 1.4606 ns | 0.0801 ns | 0.0593 ns |         - |
| Method_Group         | 1.4596 ns | 2.0504 ns | 0.1124 ns | 1.4236 ns |         - |
| Lambda_Cached        | 0.1482 ns | 1.2373 ns | 0.0678 ns | 0.1411 ns |         - |
| Lambda_Static_Cached | 0.2343 ns | 1.2560 ns | 0.0688 ns | 0.2535 ns |         - |
| Method_Group_Cached  | 1.3833 ns | 2.3425 ns | 0.1284 ns | 1.3267 ns |         - |

## DemoDelegateParams

| Method                                   | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------------------------------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| Normal_Call                              | 0.0051 ns | 0.1596 ns | 0.0087 ns | 0.0000 ns | 0.001 |    0.00 |      - |         - |        0.00 |
| Lambda_With_IntParam                     | 0.1759 ns | 1.5548 ns | 0.0852 ns | 0.2203 ns | 0.047 |    0.02 |      - |         - |        0.00 |
| Lambda_Static_With_IntParam              | 0.2098 ns | 0.0624 ns | 0.0034 ns | 0.2107 ns | 0.056 |    0.00 |      - |         - |        0.00 |
| Lambda_With_CastedObjectParam            | 3.9975 ns | 0.5610 ns | 0.0307 ns | 4.0049 ns | 1.065 |    0.02 | 0.0005 |      24 B |        1.00 |
| Lambda_With_CastedObjectParam_Static     | 4.0300 ns | 1.5885 ns | 0.0871 ns | 4.0750 ns | 1.074 |    0.03 | 0.0005 |      24 B |        1.00 |
| Lambda_Captured_Local_Var                | 3.7530 ns | 1.4187 ns | 0.0778 ns | 3.7225 ns | 1.000 |    0.03 | 0.0005 |      24 B |        1.00 |
| Method_Group_Captured_Local_Function_Var | 3.7403 ns | 1.2843 ns | 0.0704 ns | 3.7117 ns | 0.997 |    0.02 | 0.0005 |      24 B |        1.00 |
| Lambda_Captured_Class_Var                | 0.1071 ns | 0.0208 ns | 0.0011 ns | 0.1075 ns | 0.029 |    0.00 |      - |         - |        0.00 |
| Method_Group_Captured_Class_Var          | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.000 |    0.00 |      - |         - |        0.00 |
| Lambda_Captured_Local_Var_Cached         | 0.6429 ns | 1.4799 ns | 0.0811 ns | 0.6504 ns | 0.171 |    0.02 |      - |         - |        0.00 |
| Lambda_Static_Captured_Local_Var_Cached  | 0.5994 ns | 0.2993 ns | 0.0164 ns | 0.5930 ns | 0.160 |    0.00 |      - |         - |        0.00 |
| Method_Group_Captured_Local_Var_Cached   | 1.5022 ns | 1.8352 ns | 0.1006 ns | 1.4480 ns | 0.400 |    0.02 |      - |         - |        0.00 |

## MethodGroupVsLambda

| Method              | Mean       | Error      | StdDev    | Gen0   | Allocated |
|-------------------- |-----------:|-----------:|----------:|-------:|----------:|
| Lambda_Capture      | 114.086 ns | 45.3948 ns | 2.4882 ns | 0.0124 |     664 B |
| MethodGroup_Capture | 119.204 ns | 47.3208 ns | 2.5938 ns | 0.0126 |     664 B |
| Lambda              |   3.700 ns |  0.4197 ns | 0.0230 ns |      - |         - |
| MethodGroup         |  91.318 ns | 29.6118 ns | 1.6231 ns | 0.0122 |     640 B |
| MethodGroup_Static  |  20.688 ns |  2.3768 ns | 0.1303 ns |      - |         - |
