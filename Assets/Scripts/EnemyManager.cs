﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    static EnemyManager mInstance;
    public static bool isQuitting = false;

    public static EnemyManager Instance
    {
        get
        {
            if(isQuitting)
            {
                return null;
            }
            if (mInstance == null)
            {
                GameObject go = new GameObject();
                mInstance = go.AddComponent<EnemyManager>();
            }
            return mInstance;
        }
    }

    public List<EnemyStats> activeEnemies = new List<EnemyStats>();


    void OnDestroy()
    {
        isQuitting = true;
    }
    
}
