using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public sealed class ObjectSerializer {

    private BinaryFormatter formatter = null;

    #region Singleton

    private static ObjectSerializer instance = null;

    public static ObjectSerializer Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new ObjectSerializer();
            }
            return instance;
        }
    }

    private ObjectSerializer()
    {
        formatter = new BinaryFormatter();
    }

    #endregion Singleton

    /// <summary>
    /// Versucht ein <see cref="object"/> vom Typ <see cref="{T}"/> aus einem <see cref="Stream"/> zu deserialisieren
    /// </summary>
    public bool TryDeserialize<T>(Stream inStream, out T output)
    {
        try
        {
            output = (T)formatter.Deserialize(inStream);
            return true;
        }
        catch
        {
            output = default(T);
            return false;
        }
    }

    /// <summary>
    /// Serialisiert ein <see cref="object"/> zu einem <see cref="Stream"/>
    /// </summary>
    public void Serialize(Stream outStream, object input)
    {
        formatter.Serialize(outStream, input);
    }
}
