#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;
using UsefulExtensions.MonoBehaviour;

public abstract class SingleValueManager<T, TValue> : MonoBehaviour where T : SingleValueManager<T, TValue>
{
    private static T _instance;
    public static T Instance => _instance;

#if ODIN_INSPECTOR
    [SerializeField, ReadOnly]
#endif

    protected TValue defaultValue;

#if ODIN_INSPECTOR
    [ShowInInspector, ReadOnly]
#endif

    protected TValue _currentValue;

    public TValue DefaultValue => defaultValue;

    public abstract TValue Value { get; set; }

    public virtual void RestoreDefault()
    {
        Value = defaultValue;
    }

    protected virtual void Awake()
    {
        ((T)this).MakeSingletonDestroyOthers(ref _instance);
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        if (Application.isPlaying)
            return;

        defaultValue = Value;
    }

    private void LateUpdate()
    {
        if (Time.frameCount % 10 != 0)
            return;

        _currentValue = Value;
    }
#endif
}
