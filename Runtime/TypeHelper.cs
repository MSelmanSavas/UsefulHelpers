using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TypeHelper
{
    public static bool IsTypeGeneric(System.Type type)
    {
        return type.IsGenericType || type.BaseType.IsGenericType;
    }

    public static bool IsGenericTypeHasArgumentType(System.Type genericType, System.Type genericArgumentType)
    {
        if (!IsTypeGeneric(genericType))
            return false;

        System.Type[] genericArguments = genericType.GetGenericArguments();

        for (int i = 0; i < genericArguments.Length; i++)
        {
            if (genericArguments[i] == genericArgumentType)
                return true;
        }

        System.Type[] baseClassGenericArguments = genericType.BaseType.GetGenericArguments();

        for (int i = 0; i < baseClassGenericArguments.Length; i++)
        {
            if (baseClassGenericArguments[i] == genericArgumentType)
                return true;
        }

        return false;
    }
}
