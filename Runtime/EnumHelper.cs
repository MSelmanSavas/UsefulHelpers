using System;
using System.Collections.Generic;

public static class EnumHelper
{
    private static readonly Dictionary<Type, int> EnumToLength = new Dictionary<Type, int>();

    public static int GetLength<T>() where T : struct, Enum
    {
        if (EnumToLength.TryGetValue(typeof(T), out var length))
        {
            return length;
        }

        length = Enum.GetNames(typeof(T)).Length;
        EnumToLength[typeof(T)] = length;

        return length;
    }
}
