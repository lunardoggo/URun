using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public static class GameTools {

    /// <summary>
    /// Gibt zurück, ob ein <see cref="GameObject"/> eine <see cref="Component"/> vom Typ <see cref="{T}"/> enthält und gibt diese <see cref="Component"/> per out-Parameter zurück
    /// </summary>
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

    /// <summary>
    /// Versucht einen Wert aus einer <see cref="SerializationInfo"/> mit dem angegebenen Namen zurückzugeben
    /// </summary>
    public static bool TryGetValue<T>(this SerializationInfo info, string name, out T output)
    {
        try
        {
            output = (T)info.GetValue(name, typeof(T));
            return true;
        }
        catch
        {
            output = default(T);
            return false;
        }
    }
}
