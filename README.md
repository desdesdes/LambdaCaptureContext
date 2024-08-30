# LambdaCaptureContext

Show different uses of delegates and their performance.

Results om my box:

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4037/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.400
  [Host]   : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  ShortRun : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

| Method               | Mean      | Error     | StdDev    | Median    |
|--------------------- |----------:|----------:|----------:|----------:|
| Normal_Call          | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
| Lambda_Only          | 0.0042 ns | 0.0795 ns | 0.0044 ns | 0.0039 ns |
| Lambda_Static_Only   | 0.0060 ns | 0.1228 ns | 0.0067 ns | 0.0047 ns |
| Method_Group         | 3.0840 ns | 0.0948 ns | 0.0052 ns | 3.0854 ns |
| Lambda_Cached        | 0.4393 ns | 0.1173 ns | 0.0064 ns | 0.4376 ns |
| Lambda_Static_Cached | 0.4419 ns | 0.0732 ns | 0.0040 ns | 0.4443 ns |
| Method_Group_Cached  | 3.0729 ns | 0.2707 ns | 0.0148 ns | 3.0788 ns |

| Method                                   | Mean       | Error      | StdDev    | Median     | Ratio | RatioSD |
|----------------------------------------- |-----------:|-----------:|----------:|-----------:|------:|--------:|
| Normal_Call                              |  0.0017 ns |  0.0145 ns | 0.0008 ns |  0.0018 ns | 0.000 |    0.00 |
| Lambda_With_IntParam                     |  0.0035 ns |  0.0572 ns | 0.0031 ns |  0.0045 ns | 0.000 |    0.00 |
| Lambda_Static_With_IntParam              |  0.0085 ns |  0.0320 ns | 0.0018 ns |  0.0075 ns | 0.001 |    0.00 |
| Lambda_With_CastedObjectParam            |  3.2199 ns |  1.7221 ns | 0.0944 ns |  3.2535 ns | 0.274 |    0.02 |
| Lambda_With_CastedObjectParam_Static     |  3.9016 ns |  1.4748 ns | 0.0808 ns |  3.9075 ns | 0.332 |    0.02 |
| Lambda_Captured_Local_Var                | 11.7998 ns | 15.7738 ns | 0.8646 ns | 11.4501 ns | 1.003 |    0.09 |
| Method_Group_Captured_Local_Function_Var | 11.6154 ns |  2.6384 ns | 0.1446 ns | 11.5573 ns | 0.988 |    0.06 |
| Lambda_Captured_Class_Var                |  6.9214 ns |  1.1862 ns | 0.0650 ns |  6.9469 ns | 0.589 |    0.04 |
| Method_Group_Captured_Class_Var          |  7.0076 ns |  3.1599 ns | 0.1732 ns |  6.9726 ns | 0.596 |    0.04 |
| Lambda_Captured_Local_Var_Cached         |  1.8514 ns |  0.2260 ns | 0.0124 ns |  1.8478 ns | 0.157 |    0.01 |
| Lambda_Static_Captured_Local_Var_Cached  |  1.8355 ns |  0.0952 ns | 0.0052 ns |  1.8363 ns | 0.156 |    0.01 |
| Method_Group_Captured_Local_Var_Cached   |  3.9275 ns |  0.0242 ns | 0.0013 ns |  3.9275 ns | 0.334 |    0.02 |
