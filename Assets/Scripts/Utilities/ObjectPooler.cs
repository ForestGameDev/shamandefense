using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObjectPooler {
    /// <summary>
    /// Prefab of object that will be pooled.
    /// </summary>
    public GameObject _prefab;
    /// <summary>
    /// Parent transform of the new poolable objects.
    /// </summary>
	public Transform _parent;
    /// <summary>
    /// Intantiate this number of objects at the start.
    /// </summary>
	public uint _poolLenght = 5;

    List<GameObject> _poolList= new List<GameObject>();


    /// <summary>
    /// Prefab and parent must be set before initialize. 
    /// </summary>
    public void Initialize()
	{
        if(_prefab == null)
        {
            Debug.LogError("ObjectPooler missing prefab");
            return;
        }
		for (int i = 0; i<_poolLenght; ++i) {
			CreateObject();
		}
	}


    public void Initialize(GameObject prefab)
    {
        this._prefab = prefab;
        Initialize();
    }
    /// <summary>
    /// Get an available object or create one. 
    /// </summary>
     public GameObject GetObject()
    {

        for (int i = 0; i < _poolList.Count; ++i)
        {
            if (!_poolList[i].activeInHierarchy)
            {
                return _poolList[i];
            }
            
        }
        int index = _poolList.Count;
        if(_prefab == null)
        {
            return null;
        }
        CreateObject();
        return _poolList[index];
    }

    public void DestroyPool()
    {
        for (int i = _poolList.Count - 1; i >= 0; --i)
        {
            GameObject.Destroy(_poolList[i]);
        }
        _poolList.Clear();
    }

    void CreateObject()
	{
		GameObject go = GameObject.Instantiate (_prefab) as GameObject;
        go.SetActive (false);

        if (_parent != null) {
			go.transform.parent = _parent;
            go.transform.position = go.transform.parent.position;
        }
		
		_poolList.Add (go);

	}



}
