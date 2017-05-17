using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameTools {

    public static T GetComponentIfNotNull<T>(this GameObject gameObject)
    {
        T output = default(T);
        if (gameObject != null)
        {
            output = gameObject.GetComponent<T>();
        }
        return output;
    }
}
