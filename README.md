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
| Lambda_Only          | 0.0074 ns | 0.1483 ns | 0.0081 ns | 0.0033 ns |         - |
| Lambda_Static_Only   | 0.0053 ns | 0.0909 ns | 0.0050 ns | 0.0061 ns |         - |
| Method_Group         | 3.0604 ns | 0.0410 ns | 0.0022 ns | 3.0595 ns |         - |
| Lambda_Cached        | 0.4459 ns | 0.1300 ns | 0.0071 ns | 0.4458 ns |         - |
| Lambda_Static_Cached | 0.6641 ns | 3.1321 ns | 0.1717 ns | 0.6835 ns |         - |
| Method_Group_Cached  | 3.1853 ns | 2.6618 ns | 0.1459 ns | 3.1549 ns |         - |

## DemoDelegateParams

| Method                                   | Mean       | Error     | StdDev    | Median     | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------------------------------------- |-----------:|----------:|----------:|-----------:|------:|--------:|-------:|----------:|------------:|
| Normal_Call                              |  0.0020 ns | 0.0315 ns | 0.0017 ns |  0.0028 ns | 0.000 |    0.00 |      - |         - |        0.00 |
| Lambda_With_IntParam                     |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns | 0.000 |    0.00 |      - |         - |        0.00 |
| Lambda_Static_With_IntParam              |  0.0110 ns | 0.2939 ns | 0.0161 ns |  0.0026 ns | 0.001 |    0.00 |      - |         - |        0.00 |
| Lambda_With_CastedObjectParam            |  3.8270 ns | 9.2228 ns | 0.5055 ns |  3.5424 ns | 0.322 |    0.04 | 0.0019 |      24 B |        0.27 |
| Lambda_With_CastedObjectParam_Static     |  3.1685 ns | 0.9879 ns | 0.0542 ns |  3.1962 ns | 0.266 |    0.01 | 0.0019 |      24 B |        0.27 |
| Lambda_Captured_Local_Var                | 11.8967 ns | 3.3391 ns | 0.1830 ns | 11.9940 ns | 1.000 |    0.02 | 0.0070 |      88 B |        1.00 |
| Method_Group_Captured_Local_Function_Var | 11.5359 ns | 3.8168 ns | 0.2092 ns | 11.5374 ns | 0.970 |    0.02 | 0.0070 |      88 B |        1.00 |
| Lambda_Captured_Class_Var                |  7.2565 ns | 1.9260 ns | 0.1056 ns |  7.2897 ns | 0.610 |    0.01 | 0.0051 |      64 B |        0.73 |
| Method_Group_Captured_Class_Var          |  6.9162 ns | 2.6153 ns | 0.1434 ns |  6.8998 ns | 0.581 |    0.01 | 0.0051 |      64 B |        0.73 |
| Lambda_Captured_Local_Var_Cached         |  1.8455 ns | 0.1588 ns | 0.0087 ns |  1.8410 ns | 0.155 |    0.00 |      - |         - |        0.00 |
| Lambda_Static_Captured_Local_Var_Cached  |  1.8606 ns | 0.1541 ns | 0.0084 ns |  1.8632 ns | 0.156 |    0.00 |      - |         - |        0.00 |
| Method_Group_Captured_Local_Var_Cached   |  3.9997 ns | 0.6208 ns | 0.0340 ns |  3.9804 ns | 0.336 |    0.01 |      - |         - |        0.00 |

## MethodGroupVsLambda

| Method              | Mean      | Error     | StdDev   | Gen0   | Allocated |
|-------------------- |----------:|----------:|---------:|-------:|----------:|
| Lambda_Capture      | 101.62 ns |  8.180 ns | 0.448 ns | 0.0528 |     664 B |
| MethodGroup_Capture | 100.67 ns | 34.804 ns | 1.908 ns | 0.0528 |     664 B |
| Lambda              |  12.53 ns |  0.854 ns | 0.047 ns |      - |         - |
| MethodGroup         | 107.15 ns | 14.443 ns | 0.792 ns | 0.0509 |     640 B |
| MethodGroup_Static  |  43.45 ns |  3.102 ns | 0.170 ns |      - |         - |