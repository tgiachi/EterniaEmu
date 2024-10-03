

using BenchmarkDotNet.Running;
using EterniaEmu.Performance.Benchmarks;

var summary = BenchmarkRunner.Run<TcpServerBenchmark>();
