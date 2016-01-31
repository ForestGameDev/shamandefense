using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolDictionary {

    public Transform targetParent;
    Dictionary<GameObject, ObjectPooler> dictionary = new Dictionary<GameObject, ObjectPooler>();

    public GameObject Get(GameObject type,Transform target, bool autoActive = true)
    {
        GameObject go = Get(type);
        if(go != null)
        {
            go.transform.position = target.position;
            go.transform.rotation = target.rotation;
            go.SetActive(autoActive);
        }
        return go;
    }


    public GameObject Get(GameObject type)
    {
        if(!dictionary.ContainsKey(type))
        {
            ObjectPooler pool = new ObjectPooler();
            pool._parent = targetParent;
            pool._prefab = type;
            pool._poolLenght = 1;
            pool.Initialize();
            dictionary.Add(type, pool);
        }
        return dictionary[type].GetObject();
    }

    public bool Clear(GameObject type)
    {
        if(dictionary.ContainsKey(type))
        {
            dictionary[type].DestroyPool();
            dictionary.Remove(type);
            return true;
        }
        return false;
    }

    public void Clear()
    {
        foreach (KeyValuePair<GameObject, ObjectPooler> entry in dictionary)
        {
            entry.Value.DestroyPool();

            // do something with entry.Value or entry.Key
        }
        dictionary.Clear();
    }

}
