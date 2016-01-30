using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    [System.Serializable]
    class EnemyProbability
    {
        public GameObject enemyPrefab;
        public int probabilityWeight;
    }


    [SerializeField] EnemyProbability[] enemyProbability;
    [SerializeField] BezierSpline[] paths;
    [SerializeField]    float timer = .1f;
    [SerializeField]    Transform exitLocation;

    PoolDictionary enemyPool = new PoolDictionary();

    [SerializeField]
    Probability<GameObject> probability = new Probability<GameObject>();


	// Use this for initialization
	void Start () {
        enemyPool.targetParent = transform;
        foreach(EnemyProbability prob in enemyProbability)
        {
            probability.Add(prob.enemyPrefab, prob.probabilityWeight);
        }
        
        InvokeRepeating("Spawn", .01f, timer);

        

    }


    void Spawn()
    {
        EnemyAI ai = enemyPool.Get(probability.Get()).GetComponent<EnemyAI>();
        int value = Random.Range(0, paths.Length);
        ai.spline = paths[value];
        ai.exitLocation = transform;
        ai.gameObject.SetActive(true);

    }
}
