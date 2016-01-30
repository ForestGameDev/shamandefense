using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] GameObject[] enemies;
    [SerializeField] BezierSpline[] paths;
    [SerializeField]    float timer = .1f;
    [SerializeField]    Transform exitLocation;

    PoolDictionary enemyPool = new PoolDictionary();



	// Use this for initialization
	void Start () {
        enemyPool.targetParent = transform;
        InvokeRepeating("Spawn", .01f, timer);

    }


    void Spawn()
    {
        int value = Random.Range(0, enemies.Length);
        EnemyAI ai = enemyPool.Get(enemies[value]).GetComponent<EnemyAI>();
        value = Random.Range(0, paths.Length);
        ai.spline = paths[value];
        ai.exitLocation = transform;
        ai.gameObject.SetActive(true);

    }
}
