using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UsefulExtensions
{
    public static class WaitFor
    {
        private static readonly Dictionary<float, WaitForSeconds> WaitCache = new Dictionary<float, WaitForSeconds>();
        private static readonly Dictionary<float, WaitForSecondsGlobalRealtime> RealtimeWaitCache = new Dictionary<float, WaitForSecondsGlobalRealtime>();

        public static readonly WaitForEndOfFrame EndOfFrame = new WaitForEndOfFrame();

        public static WaitForSeconds Seconds(float seconds)
        {
            if (seconds < float.Epsilon)
                return null;

            if (WaitCache.TryGetValue(seconds, out var wait))
                return wait;

            wait = new WaitForSeconds(seconds);

            if (seconds.IsMultipleOf(.125f) || seconds.IsMultipleOf(.1f) || seconds.IsMultipleOf(.01f))
            {
                WaitCache[seconds] = wait;
            }

            return wait;
        }

        public static WaitForSeconds Seconds(int min, int max)
        {
            int seconds = Random.Range(min, max);
            return Seconds(seconds);
        }

        public static void ClearCache() => WaitCache.Clear();

        private static bool IsMultipleOf(this float n, float multipleOf)
        {
            float remainder = n % multipleOf;

            if (Mathf.Approximately(remainder, 0))
                return true;

            if (Mathf.Approximately(multipleOf, remainder))
                return true;

            return false;
        }

        public static WaitForSecondsGlobalRealtime RealtimeSeconds(float seconds)
        {
            if (seconds < float.Epsilon)
                return null;

            if (RealtimeWaitCache.TryGetValue(seconds, out var wait))
                return wait;

            wait = new WaitForSecondsGlobalRealtime(seconds);

            if (seconds.IsMultipleOf(.125f) || seconds.IsMultipleOf(.1f) || seconds.IsMultipleOf(.01f))
            {
                RealtimeWaitCache[seconds] = wait;
            }

            return wait;
        }

          public static WaitForSecondsGlobalRealtime RealtimeSeconds(int min, int max)
        {
            int seconds = Random.Range(min, max);
            return RealtimeSeconds(seconds);
        }

        public static void ClearRealtimeCache() => RealtimeWaitCache.Clear();

    }

}
