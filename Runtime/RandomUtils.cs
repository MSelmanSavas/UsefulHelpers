
using UnityEngine;

public static class RandomUtils
{
    public static bool Percentage(int percentage)
    {
        return Random.Range(0, 100) < percentage;
    }
}
