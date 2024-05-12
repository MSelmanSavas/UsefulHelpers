using UnityEngine;
using UsefulExtensions.MonoBehaviour;

public sealed class TargetFramerateSetter : MonoBehaviour
{
    public static TargetFramerateSetter Instance => _instance;
    private static TargetFramerateSetter _instance;

    public int targetFramerate = Application.isEditor ? 300 : 60;

    private void Awake()
    {
        if (!this.MakeSingletonDestroyOthers(ref _instance))
        {
            return;
        }
    }

    private void Update()
    {
        if (Application.targetFrameRate != targetFramerate)
        {
            //Application.targetFrameRate = targetFramerate;
        }
    }
}