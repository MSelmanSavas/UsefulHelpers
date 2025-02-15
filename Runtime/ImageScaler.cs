using System;
using System.Collections;
using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;
using UsefulExtensions.MonoBehaviour;

public class ImageScaler : MonoBehaviour
{
    [SerializeField] private RectTransform image;
    [SerializeField] private AnimationCurve curve;

    private IEnumerator _currentCoroutine;
    public IEnumerator CurrentCoroutine => _currentCoroutine;

    private IEnumerator ScaleCoroutine(float duration, float delay, bool reverse = false)
    {
        float waitUntil = Time.time + delay;
        while (Time.time < waitUntil)
            yield return null;

        var evaluate = reverse
            ? new Func<AnimationCurve, float, float>((c, t) => c.Evaluate(1 - t))
            : new Func<AnimationCurve, float, float>((c, t) => c.Evaluate(t));

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            float value = evaluate(curve, t);
            image.localScale = new Vector3(value, value);

            yield return null;
        }

        image.localScale = curve.Evaluate(reverse ? 0 : 1) * image.localScale;
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    public void Scale(float duration = 1, float delay = 0)
    {
        this.StopStartCoroutine(ref _currentCoroutine, ScaleCoroutine(duration, delay));
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    public void ScaleReverse(float duration = 1, float delay = 0)
    {
        this.StopStartCoroutine(ref _currentCoroutine, ScaleCoroutine(duration, delay, true));
    }

    public void ScaleReverseIfVisible(float duration = 1, float delay = 0)
    {
        if (image.localScale.x < 0.01f)
            return;

        ScaleReverse(duration, delay);

    }

    public void ResetScale()
    {
        Vector3 scaleZero = new Vector3(0, 0, 0);
        if (image.localScale.x < 0.02f)
        {
            image.localScale = scaleZero;
        }

    }
}
