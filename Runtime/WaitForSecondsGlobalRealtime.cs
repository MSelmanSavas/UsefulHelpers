using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can be used for runtime and editor coroutines.
/// </summary>
public class WaitForSecondsGlobalRealtime : CustomYieldInstruction
{
    public float waitTime { get; set; }
    float m_WaitUntilTime = -1;

    public override bool keepWaiting
    {
        get
        {
            if (m_WaitUntilTime < 0)
            {
                m_WaitUntilTime = Time.realtimeSinceStartup + waitTime;
            }

            bool wait = Time.realtimeSinceStartup < m_WaitUntilTime;

            if (!wait)
            {
                // Reset so it can be reused.
                Reset();
            }

            return wait;
        }
    }

    public WaitForSecondsGlobalRealtime(float time)
    {
        waitTime = time;
    }

    public override void Reset()
    {
        m_WaitUntilTime = -1;
    }
}
