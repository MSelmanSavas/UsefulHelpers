using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableFinder : MonoBehaviour
{
    private void OnEnable()
    {
#if UNITY_EDITOR
        Debug.LogError("<color=#00ff00ff>" + gameObject.name + " is enabled! Please remove this script after use THANK YOU!</color>", gameObject);
#endif

#if !UNITY_EDITOR
		Debug.LogError(gameObject.name + "is enabled! Please remove this script after use THANK YOU!");
#endif
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        Debug.LogError("<color=#ffffffff>" + gameObject.name + " is disabled! Please remove this script after use THANK YOU!</color>", gameObject);
#endif

#if !UNITY_EDITOR
		Debug.LogError(gameObject.name + "is disabled! Please remove this script after use THANK YOU!");
#endif
    }

    private void OnDestroy()
    {

        {
#if UNITY_EDITOR
            Debug.LogErrorFormat("<color=#ffffffff>" + gameObject.name + "is destroyed! Please remove this script after use THANK YOU!</color>", gameObject);
#endif

#if !UNITY_EDITOR
		Debug.LogError(gameObject.name + "is disabled! Please remove this script after use THANK YOU!");
#endif

        }
    }
}
