using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameTools {

    public static bool TryGetComponent<T>(this GameObject obj, out T component)
    {
        if (obj != null)
        {
            component = obj.GetComponent<T>();
            if (component != null)
            {
                component = obj.GetComponent<T>();
                return true;
            }
        }
        component = default(T);
        return false;
    }
}
