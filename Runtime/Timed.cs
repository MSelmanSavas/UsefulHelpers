using System;
using System.Diagnostics;

public sealed class Timed<T>
{
    public T Value { get; }
    public TimeSpan ElapsedTime { get; }

    private Timed(T value, TimeSpan elapsedTime)
    {
        Value = value;
        ElapsedTime = elapsedTime;
    }

    public static Timed<T> Create(Func<T> create)
    {
        DateTime startTime = DateTime.UtcNow;
        T result = create();
        TimeSpan elapsed = DateTime.UtcNow - startTime;

        return new Timed<T>(result, elapsed);
    }

    public static Timed<T> Create<TIn>(Func<TIn, T> create, TIn input)
    {
        DateTime startTime = DateTime.UtcNow;
        T result = create(input);
        TimeSpan elapsed = DateTime.UtcNow - startTime;

        return new Timed<T>(result, elapsed);
    }

    public static Timed<T> CreateAccurate(Func<T> create)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        T result = create();
        stopwatch.Stop();

        return new Timed<T>(result, stopwatch.Elapsed);
    }

    public static Timed<T> CreateAccurate<TIn>(Func<TIn, T> create, TIn input)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        T result = create(input);
        stopwatch.Stop();

        return new Timed<T>(result, stopwatch.Elapsed);
    }
}

public sealed class Timed
{
    public TimeSpan ElapsedTime { get; }

    private Timed(TimeSpan elapsedTime)
    {
        ElapsedTime = elapsedTime;
    }

    public static Timed Create(Action create)
    {
        DateTime startTime = DateTime.UtcNow;
        create();
        TimeSpan elapsed = DateTime.UtcNow - startTime;

        return new Timed(elapsed);
    }

    public static Timed Create<TIn>(Action<TIn> create, TIn input)
    {
        DateTime startTime = DateTime.UtcNow;
        create(input);
        TimeSpan elapsed = DateTime.UtcNow - startTime;

        return new Timed(elapsed);
    }

    public static Timed CreateAccurate(Action create)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        create();
        stopwatch.Stop();

        return new Timed(stopwatch.Elapsed);
    }

    public static Timed CreateAccurate<TIn>(Action<TIn> create, TIn input)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        create(input);
        stopwatch.Stop();

        return new Timed(stopwatch.Elapsed);
    }
}
